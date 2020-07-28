using Microsoft.EntityFrameworkCore.Migrations;

namespace TastyBoutique.Persistance.Migrations
{
    public partial class addedDeleteBehavior_Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Recipes",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeComment_Recipes",
                table: "RecipeComment");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipesFilters_Recipes",
                table: "RecipesFilters");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipesIngredients_Recipes",
                table: "RecipesIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeType_Recipes",
                table: "RecipeType");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedRecipes_Recipes",
                table: "SavedRecipes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Recipes",
                table: "Notifications",
                column: "IdRecipe",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeComment_Recipes",
                table: "RecipeComment",
                column: "IdRecipe",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesFilters_Recipes",
                table: "RecipesFilters",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesIngredients_Recipes",
                table: "RecipesIngredients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeType_Recipes",
                table: "RecipeType",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedRecipes_Recipes",
                table: "SavedRecipes",
                column: "IdRecipe",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Recipes",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeComment_Recipes",
                table: "RecipeComment");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipesFilters_Recipes",
                table: "RecipesFilters");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipesIngredients_Recipes",
                table: "RecipesIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeType_Recipes",
                table: "RecipeType");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedRecipes_Recipes",
                table: "SavedRecipes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Recipes",
                table: "Notifications",
                column: "IdRecipe",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeComment_Recipes",
                table: "RecipeComment",
                column: "IdRecipe",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesFilters_Recipes",
                table: "RecipesFilters",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesIngredients_Recipes",
                table: "RecipesIngredients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeType_Recipes",
                table: "RecipeType",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedRecipes_Recipes",
                table: "SavedRecipes",
                column: "IdRecipe",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
