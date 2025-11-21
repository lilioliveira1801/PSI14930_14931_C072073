using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSI14930_c072073.Context;

namespace PSI14930_c072073.Controllers;


[ApiController]
[Route("[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly SimulacaoInvestimentoPerfilContext _context;

    public ProdutoController(SimulacaoInvestimentoPerfilContext context)
    {
        _context = context;
    }

    // GET: api/produtos
    [HttpGet("Consultar produtos de investimento")]
    public async Task<IActionResult> GetProdutos()
    {
        try
        {
            var produtos = await _context.Produtos.ToListAsync();

            if (produtos == null || !produtos.Any())
                return NotFound("Nenhum produto encontrado.");

            return Ok(produtos);
        }
        catch (Exception ex)
        {
            // Aqui você pode logar o erro se tiver um logger configurado
            return StatusCode(500, $"Erro interno ao buscar produtos: {ex.Message}");
        }
    }

    
    [HttpGet("ConsultarProdutoInvestimentoPeloIdentificador/{id}")]
    public async Task<IActionResult> GetProduto(int id)
    {
        try
        {
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return NotFound($"Produto com Id {id} não encontrado. Favor consultar a lista de produtos.");

            return Ok(produto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao buscar produto: {ex.Message}");
        }
    }
    [HttpGet("ProdutoPorPerfil/{perfil}")]
    public async Task<IActionResult> GetProdutosPorPerfil(string perfil)
    {
        try
        {
            string risco;

            switch (perfil.ToLower())
            {
                case "conservador":
                    risco = "Baixo";
                    break;
                case "moderado":
                    risco = "Médio";
                    break;
                case "arrojado":
                    risco = "Alto";
                    break;
                default:
                    return BadRequest(new
                    {
                        mensagem = "Perfil inválido. Use: conservador, moderado ou arrojado."
                    });
            }

            var produtos = await _context.Produtos
                .Where(p => p.Risco.ToLower() == risco.ToLower())
                .ToListAsync();

            var response = new
            {
                servicos = new[]
                {
                        new {
                            quantidadeProdutos = produtos.Count,
                            produtos = produtos.Select(p => new {
                                p.Id,
                                p.Nome,
                                p.Tipo,
                                p.Rentabilidade,
                                p.Risco
                            })
                        }
                    }
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                mensagem = "Erro interno ao consultar produtos.",
                detalhe = ex.Message
            });
        }
    }
}

