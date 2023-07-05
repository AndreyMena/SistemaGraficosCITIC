using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGraficosCITIC.Migrations
{
    public partial class ActiveColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Project",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Project");
        }
    }
}
