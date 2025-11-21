using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSI14930_c072073.Migrations
{
    /// <inheritdoc />
    public partial class AddHistoricoInvestimentoClienteFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HistoricoInvestimentos_IdCliente",
                table: "HistoricoInvestimentos",
                column: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoInvestimentos_Clientes_IdCliente",
                table: "HistoricoInvestimentos",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoInvestimentos_Clientes_IdCliente",
                table: "HistoricoInvestimentos");

            migrationBuilder.DropIndex(
                name: "IX_HistoricoInvestimentos_IdCliente",
                table: "HistoricoInvestimentos");
        }
    }
}
