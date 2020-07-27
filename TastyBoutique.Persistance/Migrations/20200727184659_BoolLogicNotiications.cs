using Microsoft.EntityFrameworkCore.Migrations;

namespace TastyBoutique.Persistance.Migrations
{
    public partial class BoolLogicNotiications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "SavedRecipes");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Recipes");

            migrationBuilder.AddColumn<bool>(
                name: "NeedUpdate",
                table: "SavedRecipes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeedUpdate",
                table: "SavedRecipes");

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "SavedRecipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
