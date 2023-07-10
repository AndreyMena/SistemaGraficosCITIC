using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGraficosCITIC.Migrations
{
    public partial class FixcolumnnameinProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Markeatable",
                table: "Product",
                newName: "Marketable");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Marketable",
                table: "Product",
                newName: "Markeatable");
        }
    }
}
