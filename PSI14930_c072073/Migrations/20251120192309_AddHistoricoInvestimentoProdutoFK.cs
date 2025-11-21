using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSI14930_c072073.Migrations
{
    /// <inheritdoc />
    public partial class AddHistoricoInvestimentoProdutoFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HistoricoInvestimentos_IdProduto",
                table: "HistoricoInvestimentos",
                column: "IdProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoInvestimentos_Produtos_IdProduto",
                table: "HistoricoInvestimentos",
                column: "IdProduto",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoInvestimentos_Produtos_IdProduto",
                table: "HistoricoInvestimentos");

            migrationBuilder.DropIndex(
                name: "IX_HistoricoInvestimentos_IdProduto",
                table: "HistoricoInvestimentos");
        }
    }
}
