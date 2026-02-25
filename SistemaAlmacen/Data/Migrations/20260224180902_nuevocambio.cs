using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAlmacen.Migrations
{
    /// <inheritdoc />
    public partial class nuevocambio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioCompra",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "PrecioVenta",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Unidad",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Nota",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "PrecioCompraMov",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "PrecioVentaMov",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "Referencia",
                table: "Movimientos");

            migrationBuilder.RenameColumn(
                name: "StockMinimo",
                table: "Productos",
                newName: "Existencias");

            migrationBuilder.AlterColumn<string>(
                name: "Observaciones",
                table: "Movimientos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Destino",
                table: "Movimientos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioCompra",
                table: "Movimientos",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destino",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "PrecioCompra",
                table: "Movimientos");

            migrationBuilder.RenameColumn(
                name: "Existencias",
                table: "Productos",
                newName: "StockMinimo");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioCompra",
                table: "Productos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioVenta",
                table: "Productos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Unidad",
                table: "Productos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observaciones",
                table: "Movimientos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nota",
                table: "Movimientos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioCompraMov",
                table: "Movimientos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioVentaMov",
                table: "Movimientos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Referencia",
                table: "Movimientos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
