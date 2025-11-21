using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSI14930_c072073.Context;
using PSI14930_c072073.DTO;
using PSI14930_c072073.Logica;
using PSI14930_c072073.Model;
using System.Diagnostics;
//using static PSI14930_c072073.DTO.SimulacaoInvestimentoDTO;


namespace PSI14930_c072073.Controllers;

[ApiController]
[Route("[controller]")]
public class InvestimentoController : ControllerBase
{
    private readonly SimulacaoInvestimentoPerfilContext _context;

    public InvestimentoController(SimulacaoInvestimentoPerfilContext context)
    {
        _context = context;
    }
    [HttpPost("Simular Investimento")]
    public async Task<IActionResult> Simular([FromBody] SimulacaoRequest request)
    {
        try
        {
            // Validar cliente
            var cliente = await _context.Clientes.FindAsync(request.ClienteId);
            if (cliente == null)
                return NotFound($"Cliente com Id {request.ClienteId} não encontrado.Consulte a lista de clientes.");

            // Validar produto
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Tipo == request.TipoProduto);
            if (produto == null)
                return NotFound($"Produto do tipo {request.TipoProduto} não encontrado.Consulte a lista de produtos.");

            
            double rentabilidade = (double)produto.Rentabilidade;
            double valorFinal = request.Valor * (1 + rentabilidade);
            

            var response = new SimulacaoResponse
            {
                ProdutoValidado = new ProdutoValidadoResponse
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Tipo = produto.Tipo,
                    Rentabilidade = rentabilidade,
                    Risco = produto.Risco
                },
                ResultadoSimulacao = new ResultadoSimulacaoResponse
                {
                    ValorFinal = Math.Round(valorFinal, 2),
                    RentabilidadeEfetiva = rentabilidade,
                    PrazoMeses = request.PrazoMeses
                },
                DataSimulacao = DateTime.UtcNow
            };

            // Gravar no histórico
            var historico = new HistoricoSimulacaoInvestimento
            {
                IdCliente = request.ClienteId,
                IdProduto = produto.Id,
                ValorInvestido = request.Valor,
                ValorFinal = valorFinal,
                Prazo = request.PrazoMeses,
                Data = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            _context.HistoricoSimulacaoInvestimentos.Add(historico);
            await _context.SaveChangesAsync();

        
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao simular investimento: {ex.Message}");
        }
    }

    [HttpGet("Histórico de Simulacões")]
    public async Task<IActionResult> GetSimulacoes()
    {
        try
        {
            var simulacoes = await _context.HistoricoSimulacaoInvestimentos
                .Include(h => h.Cliente)
                .Include(h => h.Produto)
                .Select(h => new
                {
                    id = h.Id,
                    clienteId = h.IdCliente,
                    produto = h.Produto.Nome, // pega só o nome do produto
                    valorInvestido = h.ValorInvestido,
                    valorFinal = Math.Round(h.ValorFinal,2),
                    prazoMeses = h.Prazo,
                    dataSimulacao = h.Data.ToDateTime(TimeOnly.MinValue).ToString("yyyy-MM-ddTHH:mm:ssZ")
                })
                .ToListAsync();

            if (simulacoes == null || !simulacoes.Any())
                return NotFound("Nenhuma simulação encontrada.");

            return Ok(simulacoes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao consultar simulações: {ex.Message}");
        }
    }
    [HttpGet("Quantidade de simulações por produto a cada dia")]
    public async Task<IActionResult> GetSimulacoesPorProdutoDia()
    {
        try
        {
            var simulacoesAgrupadas = await _context.HistoricoSimulacaoInvestimentos
                .Include(h => h.Produto)
                .GroupBy(h => new { h.Produto.Nome, h.Data })
                .Select(g => new
                {
                    produto = g.Key.Nome,
                    data = g.Key.Data.ToString("yyyy-MM-dd"),
                    quantidadeSimulacoes = g.Count(),
                    mediaValorFinal = Math.Round(g.Average(x => x.ValorFinal), 2)
                })
                .ToListAsync();

            if (simulacoesAgrupadas == null || !simulacoesAgrupadas.Any())
                return NotFound("Nenhuma simulação encontrada.");

            return Ok(simulacoesAgrupadas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao consultar simulações agrupadas: {ex.Message}");
        }
    }

    [HttpPost("Simular Perfil")]
    public async Task<IActionResult> SimularFerfil([FromBody] SimulacaoPerfilDTO request)
    {
        // Criar objeto
        var simulacao = new HistoricoSimulacaoPerfil
        {
            Valor = request.Valor,
            Prazo = request.Prazo,
            Liquidez = request.Liquidez,
            Rentabilidade = request.Rentabilidade,
            Data = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        // Calcular perfil e pontuação
        var logicaPerfil =  new HistoricoSimulacaoPerfilCalculo();
        logicaPerfil.CalcularPerfil(simulacao);

        // Salvar no banco
        _context.HistoricoSimulacaoPerfis.Add(simulacao);
        await _context.SaveChangesAsync();

        return Ok(simulacao);
    }

    [HttpGet("Telemetria")]
    public async Task<IActionResult> ConsultarServicos()
    {
        // medir tempo da consulta de investimentos
        var stopwatchInvestimento = new Stopwatch();
        stopwatchInvestimento.Start();

        var qtdInvestimento = await _context.HistoricoSimulacaoInvestimentos
            .CountAsync();
        var menorDataInvestimento = await _context.HistoricoSimulacaoInvestimentos.MinAsync(h => h.Data);
        var maiorDataInvestimento = await _context.HistoricoSimulacaoInvestimentos.MaxAsync(h => h.Data);

        stopwatchInvestimento.Stop();
        var tempoInvestimentoMs = stopwatchInvestimento.ElapsedMilliseconds;

        // medir tempo da consulta de perfis
        var stopwatchPerfil = new Stopwatch();
        stopwatchPerfil.Start();

        var qtdPerfil = await _context.HistoricoSimulacaoPerfis
            .CountAsync();
        var menorDataPerfil = await _context.HistoricoSimulacaoInvestimentos.MinAsync(h => h.Data);
        var maiorDataPerfil = await _context.HistoricoSimulacaoInvestimentos.MaxAsync(h => h.Data);

        stopwatchPerfil.Stop();
        var tempoPerfilMs = stopwatchPerfil.ElapsedMilliseconds;

        // calcular período global (menor data entre as duas tabelas e maior data entre as duas tabelas)
        var menorDataGlobal = menorDataInvestimento < menorDataPerfil ? menorDataInvestimento : menorDataPerfil;
        var maiorDataGlobal = maiorDataInvestimento > maiorDataPerfil ? maiorDataInvestimento : maiorDataPerfil;

        var response = new
        {
            servicos = new[]
            {
                    new {
                        nome = "simular-investimento",
                        quantidadeChamadas = qtdInvestimento,
                        mediaTempoRespostaMs = tempoInvestimentoMs
                    },
                    new {
                        nome = "perfil-risco",
                        quantidadeChamadas = qtdPerfil,
                        mediaTempoRespostaMs = tempoPerfilMs
                    }
                },
            periodo = new
            {
                inicio = menorDataGlobal.ToString("yyyy-MM-dd"),
                fim = maiorDataGlobal.ToString("yyyy-MM-dd")
            }
        };

        return Ok(response);
    }
}


