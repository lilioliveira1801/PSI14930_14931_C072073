using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using PSI14930_c072073.Context;
using PSI14930_c072073.Controllers;
using PSI14930_c072073.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PsiTestesUnitarios.ControllerTestes;


public class ClienteTeste
{
    private readonly Mock<SimulacaoInvestimentoPerfilContext> _mockContext;
    private readonly ClienteController _controller;

    public ClienteTeste()
    {
        var options = new DbContextOptions<SimulacaoInvestimentoPerfilContext>();
        _mockContext = new Mock<SimulacaoInvestimentoPerfilContext>(options);
        _controller = new ClienteController(_mockContext.Object);
    }

    [Fact]
    public async Task GetClientes_DeveRetornarNotFound_QuandoNaoExistemClientes()
    {
        var options = new DbContextOptionsBuilder<SimulacaoInvestimentoPerfilContext>()
        .UseInMemoryDatabase(databaseName: "TestDb")
        .Options;

        using var context = new SimulacaoInvestimentoPerfilContext(options);
        var controller = new ClienteController(context);

        // não adiciona nenhum cliente
        var result = await controller.GetClientes();

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Nenhum cliente encontrado.", notFound.Value);
    }
    [Fact]
    public async Task GetClientes_DeveRetornarOk_QuandoExistemClientes()
    {
        var options = new DbContextOptionsBuilder<SimulacaoInvestimentoPerfilContext>()
            .UseInMemoryDatabase("DbTest2")
            .Options;

        using var context = new SimulacaoInvestimentoPerfilContext(options);
        context.Clientes.Add(new Cliente { Id = 1, Perfil = "Conservador", Pontuacao = 80, Descricao = "Teste" });
        context.SaveChanges();

        var controller = new ClienteController(context);

        // Act
        var result = await controller.GetClientes();

        // Assert
        var okResult = Assert.IsAssignableFrom<ObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var retorno = Assert.IsAssignableFrom<IEnumerable<Cliente>>(okResult.Value);
        Assert.Single(retorno);
    }
    [Fact]
    public async Task GetClientes_DeveRetornarErroInterno_QuandoExcecaoLancada()
    {
        var options = new DbContextOptionsBuilder<SimulacaoInvestimentoPerfilContext>()
            .UseInMemoryDatabase(databaseName: "TestDb3")
            .Options;

        using var context = new SimulacaoInvestimentoPerfilContext(options);

        // força erro: dispose antes de usar
        context.Dispose();
        var controller = new ClienteController(context);

        var result = await controller.GetClientes();

        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusResult.StatusCode);
        Assert.Contains("Erro interno ao buscar clientes", statusResult.Value.ToString());
    }

    [Fact]
    public async Task GetInvestimentosPorCliente_DeveRetornarNotFound_QuandoClienteNaoExiste()
    {
        var options = new DbContextOptionsBuilder<SimulacaoInvestimentoPerfilContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new SimulacaoInvestimentoPerfilContext(options);
        var controller = new ClienteController(context);

        var result = await controller.GetInvestimentosPorCliente(999);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Contains("não localizado", notFound.Value.ToString());
    }

    [Fact]
    public async Task GetInvestimentosPorCliente_DeveRetornarNotFound_QuandoNaoTemInvestimentos()
    {
        var options = new DbContextOptionsBuilder<SimulacaoInvestimentoPerfilContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new SimulacaoInvestimentoPerfilContext(options);
        context.Clientes.Add(new Cliente { Id = 1, Perfil = "Moderado", Pontuacao = 50, Descricao = "Teste" });
        context.SaveChanges();

        var controller = new ClienteController(context);

        var result = await controller.GetInvestimentosPorCliente(1);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Contains("Nenhum investimento encontrado", notFound.Value.ToString());
    }
    [Fact]
    public async Task GetInvestimentosPorCliente_DeveRetornarErroInterno_QuandoExcecaoLancada()
    {
        var options = new DbContextOptionsBuilder<SimulacaoInvestimentoPerfilContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new SimulacaoInvestimentoPerfilContext(options);
        var controller = new ClienteController(context);

        // força erro: dispose antes de usar
        context.Dispose();

        var result = await controller.GetInvestimentosPorCliente(1);

        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusResult.StatusCode);
        Assert.Contains("Erro interno ao consultar investimentos", statusResult.Value.ToString());
    }
}
