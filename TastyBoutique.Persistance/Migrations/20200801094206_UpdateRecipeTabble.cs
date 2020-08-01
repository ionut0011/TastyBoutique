using Microsoft.EntityFrameworkCore.Migrations;

namespace TastyBoutique.Persistance.Migrations
{
    public partial class UpdateRecipeTabble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Notifications",
                table: "Recipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Recipes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notifications",
                table: "Recipes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}
