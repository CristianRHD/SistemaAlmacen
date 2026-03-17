using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAlmacen.Migrations
{
    /// <inheritdoc />
    public partial class AgregarUnidadesPorLote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnidadesPorLote",
                table: "Productos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnidadesPerdidas",
                table: "Movimientos",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnidadesPorLote",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "UnidadesPerdidas",
                table: "Movimientos");
        }
    }
}
