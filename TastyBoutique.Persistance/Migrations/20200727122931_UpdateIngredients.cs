using Microsoft.EntityFrameworkCore.Migrations;

namespace TastyBoutique.Persistance.Migrations
{
    public partial class UpdateIngredients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Ingredients");

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "SavedRecipes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Recipes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipesIngredients",
                table: "RecipesIngredients",
                columns: new[] { "RecipeId", "IngredientId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipesFilters",
                table: "RecipesFilters",
                columns: new[] { "RecipeId", "FilterId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipesIngredients",
                table: "RecipesIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipesFilters",
                table: "RecipesFilters");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "SavedRecipes");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Recipes");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Ingredients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
