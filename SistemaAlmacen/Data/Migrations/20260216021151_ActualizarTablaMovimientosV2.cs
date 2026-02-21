using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAlmacen.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarTablaMovimientosV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "ProveedorId",
                table: "Movimientos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioResponsable",
                table: "Movimientos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_ProveedorId",
                table: "Movimientos",
                column: "ProveedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Proveedores_ProveedorId",
                table: "Movimientos",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Proveedores_ProveedorId",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_ProveedorId",
                table: "Movimientos");

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
                name: "ProveedorId",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "UsuarioResponsable",
                table: "Movimientos");
        }
    }
}
