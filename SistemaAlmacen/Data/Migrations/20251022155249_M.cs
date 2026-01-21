using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAlmacen.Migrations
{
    /// <inheritdoc />
    public partial class M : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioUnitario",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Movimientos");

            migrationBuilder.RenameColumn(
                name: "TipoMovimiento",
                table: "Movimientos",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "FechaMovimiento",
                table: "Movimientos",
                newName: "Fecha");

            migrationBuilder.AddColumn<string>(
                name: "Referencia",
                table: "Movimientos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Referencia",
                table: "Movimientos");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Movimientos",
                newName: "TipoMovimiento");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Movimientos",
                newName: "FechaMovimiento");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioUnitario",
                table: "Movimientos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Movimientos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Movimientos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
