using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSI14930_c072073.Migrations
{
    /// <inheritdoc />
    public partial class AddSimulacaoInvestimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HistoricoSimulacaoInvestimentos_IdCliente",
                table: "HistoricoSimulacaoInvestimentos",
                column: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoSimulacaoInvestimentos_Clientes_IdCliente",
                table: "HistoricoSimulacaoInvestimentos",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoSimulacaoInvestimentos_Clientes_IdCliente",
                table: "HistoricoSimulacaoInvestimentos");

            migrationBuilder.DropIndex(
                name: "IX_HistoricoSimulacaoInvestimentos_IdCliente",
                table: "HistoricoSimulacaoInvestimentos");
        }
    }
}
