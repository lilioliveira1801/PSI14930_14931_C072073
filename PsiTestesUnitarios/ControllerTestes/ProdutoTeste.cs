using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSI14930_c072073.Context;
using PSI14930_c072073.Controllers;
using PSI14930_c072073.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsiTestesUnitarios.ControllerTestes;
public class ProdutoTeste
{
    [Fact]
    public async Task GetProdutos_DeveRetornarNotFound_QuandoNaoExistemProdutos()
    {
        var options = new DbContextOptionsBuilder<SimulacaoInvestimentoPerfilContext>()
            .UseInMemoryDatabase("DbProdutos1")
            .Options;

        using var context = new SimulacaoInvestimentoPerfilContext(options);
        var controller = new ProdutoController(context); 

        var result = await controller.GetProdutos();

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Nenhum produto encontrado.", notFound.Value);

    }

    [Fact]
    public async Task GetProdutos_DeveRetornarOk_QuandoExistemProdutos()
    {
        var options = new DbContextOptionsBuilder<SimulacaoInvestimentoPerfilContext>()
            .UseInMemoryDatabase("DbProdutos2")
            .Options;

        using var context = new SimulacaoInvestimentoPerfilContext(options);

        context.Produtos.Add(new Produto { Id = 1, Nome = "CDB", Tipo = "Renda Fixa", Rentabilidade = 0.05m, Risco = "Baixo" });
        context.Produtos.Add(new Produto { Id = 2, Nome = "Ações", Tipo = "Ações na Bolsa", Rentabilidade = 0.12m, Risco = "Alto"});
        context.SaveChanges();

        var controller = new ProdutoController(context); 

        var result = await controller.GetProdutos();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var produtos = Assert.IsAssignableFrom<IEnumerable<Produto>>(okResult.Value);
        Assert.Equal(2, produtos.Count());
    }
    [Fact]
    public async Task GetProdutos_DeveRetornarErroInterno_QuandoExcecaoLancada()
    {
        var options = new DbContextOptionsBuilder<SimulacaoInvestimentoPerfilContext>()
            .UseInMemoryDatabase("DbProdutos3")
            .Options;

        using var context = new SimulacaoInvestimentoPerfilContext(options);
        var controller = new ProdutoController(context); 
        // força erro: dispose antes de usar
        context.Dispose();

        var result = await controller.GetProdutos();

        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusResult.StatusCode);
        Assert.Contains("Erro interno ao buscar produtos", statusResult.Value.ToString());
    }
}