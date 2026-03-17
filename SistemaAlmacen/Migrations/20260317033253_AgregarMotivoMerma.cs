using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAlmacen.Migrations
{
    /// <inheritdoc />
    public partial class AgregarMotivoMerma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MotivoMerma",
                table: "Movimientos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotivoMerma",
                table: "Movimientos");
        }
    }
}
