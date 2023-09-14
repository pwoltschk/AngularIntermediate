using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class DetachProjectFromWorkItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Projects_ProjectId",
                table: "WorkItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "WorkItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Projects_ProjectId",
                table: "WorkItems",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Projects_ProjectId",
                table: "WorkItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "WorkItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Projects_ProjectId",
                table: "WorkItems",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
