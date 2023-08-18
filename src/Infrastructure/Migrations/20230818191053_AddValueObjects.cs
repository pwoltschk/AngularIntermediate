using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddValueObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stage",
                table: "WorkItems",
                newName: "StageId");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "WorkItems",
                newName: "PriorityLevel");

            migrationBuilder.AddColumn<string>(
                name: "PriorityName",
                table: "WorkItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StageName",
                table: "WorkItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriorityName",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "StageName",
                table: "WorkItems");

            migrationBuilder.RenameColumn(
                name: "StageId",
                table: "WorkItems",
                newName: "Stage");

            migrationBuilder.RenameColumn(
                name: "PriorityLevel",
                table: "WorkItems",
                newName: "Priority");
        }
    }
}
