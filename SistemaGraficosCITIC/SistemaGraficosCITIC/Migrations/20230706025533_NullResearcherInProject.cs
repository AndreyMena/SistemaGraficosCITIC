using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGraficosCITIC.Migrations
{
    public partial class NullResearcherInProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Researcher_ResearcherId",
                table: "Project");

            migrationBuilder.AlterColumn<Guid>(
                name: "ResearcherId",
                table: "Project",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Researcher_ResearcherId",
                table: "Project",
                column: "ResearcherId",
                principalTable: "Researcher",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Researcher_ResearcherId",
                table: "Project");

            migrationBuilder.AlterColumn<Guid>(
                name: "ResearcherId",
                table: "Project",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Researcher_ResearcherId",
                table: "Project",
                column: "ResearcherId",
                principalTable: "Researcher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
