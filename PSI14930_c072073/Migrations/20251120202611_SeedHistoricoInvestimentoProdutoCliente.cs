using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSI14930_c072073.Migrations
{
    /// <inheritdoc />
    public partial class SeedHistoricoInvestimentoProdutoCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Perfil",
                table: "HistoricoSimulacaoPerfis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Perfil",
                table: "HistoricoSimulacaoPerfis");
        }
    }
}
