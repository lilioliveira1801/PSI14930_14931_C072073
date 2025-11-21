using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSI14930_c072073.Migrations
{
    /// <inheritdoc />
    public partial class AddSimulacaoInvestimentoProdutoFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HistoricoSimulacaoInvestimentos_IdProduto",
                table: "HistoricoSimulacaoInvestimentos",
                column: "IdProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoSimulacaoInvestimentos_Produtos_IdProduto",
                table: "HistoricoSimulacaoInvestimentos",
                column: "IdProduto",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoSimulacaoInvestimentos_Produtos_IdProduto",
                table: "HistoricoSimulacaoInvestimentos");

            migrationBuilder.DropIndex(
                name: "IX_HistoricoSimulacaoInvestimentos_IdProduto",
                table: "HistoricoSimulacaoInvestimentos");
        }
    }
}
