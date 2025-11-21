using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSI14930_c072073.Context;
using PSI14930_c072073.DTO;
using PSI14930_c072073.Model;

namespace PSI14930_c072073.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    private readonly SimulacaoInvestimentoPerfilContext _context;

    public ClienteController(SimulacaoInvestimentoPerfilContext context)
    {
        _context = context;
    }

    [HttpGet("Consultar Clientes")]
    public async Task<IActionResult> GetClientes()
    {
        try
        {
            var clientes = await _context.Clientes.ToListAsync();

            if (clientes == null || !clientes.Any())
                return NotFound("Nenhum cliente encontrado.");

            return Ok(clientes);
        }
        catch (Exception ex)
        {
          
            return StatusCode(500, $"Erro interno ao buscar clientes: {ex.Message}");
        }
    }

    
    [HttpGet("ConsultarPerfilCliente/{id}")]
    public async Task<IActionResult> GetCliente(int id)
    {
        try
        {
            var cliente = await _context.Clientes
            .Where(c => c.Id == id)
            .Select(c => new ClienteDTO
            {
                Id = c.Id,
                Perfil = c.Perfil,
                Pontuacao = c.Pontuacao,
                Descricao = c.Descricao
            })
            .FirstOrDefaultAsync();

            if (cliente == null)
                return NotFound($"Cliente com Id {id} não encontrado.Consulte a lista de clientes.");

            return Ok(cliente);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao buscar cliente: {ex.Message}");
        }
    }
    [HttpGet("ConsultarInvestimosCliente/{clienteId}")]
    public async Task<IActionResult> GetInvestimentosPorCliente(int clienteId)
    {
        try
        {
            var clienteExiste = await _context.Clientes.AnyAsync(c => c.Id == clienteId);
            if (!clienteExiste)
            {
                return NotFound(new
                {
                    mensagem = $"Cliente com o código {clienteId} não localizado."
                });
            }

                var investimentos = await _context.HistoricoInvestimentos
                .Include(h => h.Produto)
                .Where(h => h.IdCliente == clienteId)
                .Select(h => new
                {
                    id = h.Id,
                    tipo = h.Produto.Tipo, 
                    valor = h.Valor,
                    rentabilidade = h.Produto.Rentabilidade,
                    data = h.Data.ToString("yyyy-MM-dd")
                })
                .ToListAsync();

                           

            if (!investimentos.Any())
            {
                return NotFound(new
                {
                    mensagem = $"Nenhum investimento encontrado para o cliente {clienteId}."
                });
            }

            return Ok(investimentos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                mensagem = "Erro interno ao consultar investimentos.",
                detalhe = ex.Message
            });
        }
    }
}


