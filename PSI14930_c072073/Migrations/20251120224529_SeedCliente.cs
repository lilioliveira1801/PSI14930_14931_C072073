using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PSI14930_c072073.Migrations
{
    /// <inheritdoc />
    public partial class SeedCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Descricao", "Perfil", "Pontuacao" },
                values: new object[,]
                {
                    { 1, "Cliente que busca segurança", "Conservador", 50 },
                    { 2, "Cliente com perfil equilibrado entre segurança e rentabilidade", "Moderado", 70 },
                    { 3, "Cliente que procura alta rentabilidade", "Agressivo", 90 }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Nome", "Rentabilidade", "Risco", "Tipo" },
                values: new object[,]
                {
                    { 1, "CDB Caixa 2026", 0.12m, "Baixo", "CDB" },
                    { 2, "FundoXPTO", 0.10m, "Médio", "Fundo" },
                    { 3, "Ações", 0.20m, "Alto", "Bolsa de Valores" }
                });

            migrationBuilder.InsertData(
                table: "HistoricoInvestimentos",
                columns: new[] { "Id", "Data", "IdCliente", "IdProduto", "Valor" },
                values: new object[,]
                {
                    { 1, new DateOnly(2025, 1, 10), 1, 1, 1000.0 },
                    { 2, new DateOnly(2025, 2, 15), 1, 2, 1500.0 },
                    { 3, new DateOnly(2025, 3, 20), 2, 3, 2000.0 },
                    { 4, new DateOnly(2025, 4, 25), 2, 2, 3500.0 },
                    { 5, new DateOnly(2025, 5, 30), 3, 3, 6000.0 },
                    { 6, new DateOnly(2025, 6, 5), 3, 2, 2000.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HistoricoInvestimentos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HistoricoInvestimentos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HistoricoInvestimentos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HistoricoInvestimentos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HistoricoInvestimentos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "HistoricoInvestimentos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
