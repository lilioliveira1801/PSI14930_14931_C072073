using Microsoft.EntityFrameworkCore;
using PSI14930_c072073.Model;


namespace PSI14930_c072073.Context;

public class SimulacaoInvestimentoPerfilContext : Microsoft.EntityFrameworkCore.DbContext
{
    public SimulacaoInvestimentoPerfilContext(DbContextOptions<SimulacaoInvestimentoPerfilContext> options) : base(options) { }

    //marcar virtual para fazer os testes
    public  Microsoft.EntityFrameworkCore.DbSet<Cliente> Clientes { get; set; }
    public  Microsoft.EntityFrameworkCore.DbSet<HistoricoInvestimento> HistoricoInvestimentos { get; set; }
    public  Microsoft.EntityFrameworkCore.DbSet<HistoricoSimulacaoInvestimento> HistoricoSimulacaoInvestimentos { get; set; }
    public  Microsoft.EntityFrameworkCore.DbSet<HistoricoSimulacaoPerfil> HistoricoSimulacaoPerfis { get; set; }
    public  Microsoft.EntityFrameworkCore.DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HistoricoSimulacaoInvestimento>()
            .HasOne(hsi => hsi.Cliente)
            .WithMany(c => c.Simulacoes)
            .HasForeignKey(hsi => hsi.IdCliente);

        modelBuilder.Entity<HistoricoSimulacaoInvestimento>()
            .HasOne(hsi => hsi.Produto)
            .WithMany(c => c.Simulacoes)
            .HasForeignKey(hsi => hsi.IdProduto);

        modelBuilder.Entity<HistoricoInvestimento>()
           .HasOne(hi => hi.Cliente)
           .WithMany(c => c.Investimentos)
           .HasForeignKey(hi => hi.IdCliente);

        modelBuilder.Entity<HistoricoInvestimento>()
          .HasOne(hi => hi.Produto)
          .WithMany(c => c.Investimentos)
          .HasForeignKey(hi => hi.IdProduto);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<HistoricoInvestimento>().HasData(
        // Cliente Conservador
        new HistoricoInvestimento { Id = 1, IdCliente = 1, IdProduto = 1, Valor = 1000, Data = new DateOnly(2025, 01, 10) },
        new HistoricoInvestimento { Id = 2, IdCliente = 1, IdProduto = 2, Valor = 1500, Data = new DateOnly(2025, 02, 15) },

        // Cliente Moderado
        new HistoricoInvestimento { Id = 3, IdCliente = 2, IdProduto = 3, Valor = 2000, Data = new DateOnly(2025, 03, 20) },
        new HistoricoInvestimento { Id = 4, IdCliente = 2, IdProduto = 2, Valor = 3500, Data = new DateOnly(2025, 04, 25) },

        // Cliente Agressivo
        new HistoricoInvestimento { Id = 5, IdCliente = 3, IdProduto = 3, Valor = 6000, Data = new DateOnly(2025, 05, 30) },
        new HistoricoInvestimento { Id = 6, IdCliente = 3, IdProduto = 2, Valor = 2000, Data = new DateOnly(2025, 06, 05) }
        );


        // Dados padrão para Cliente
        modelBuilder.Entity<Cliente>().HasData(
            new Cliente { Id = 1, Perfil = "Conservador", Pontuacao = 50, Descricao = "Cliente que busca segurança" },
            new Cliente { Id = 2, Perfil = "Moderado", Pontuacao = 70, Descricao = "Cliente com perfil equilibrado entre segurança e rentabilidade" },
            new Cliente { Id = 3, Perfil = "Agressivo", Pontuacao = 90, Descricao = "Cliente que procura alta rentabilidade" }
        );

        // Dados padrão para Produto
        modelBuilder.Entity<Produto>().HasData(
            new Produto { Id = 1, Nome = "CDB Caixa 2026", Tipo = "CDB", Rentabilidade = 0.12m, Risco = "Baixo" },
            new Produto { Id = 2, Nome = "FundoXPTO", Tipo = "Fundo", Rentabilidade = 0.10m, Risco = "Médio" },
            new Produto { Id = 3, Nome = "Ações", Tipo = "Bolsa de Valores", Rentabilidade = 0.20m, Risco = "Alto" }
        );
    }


}
