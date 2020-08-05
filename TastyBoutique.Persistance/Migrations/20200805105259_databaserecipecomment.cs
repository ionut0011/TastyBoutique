using Microsoft.EntityFrameworkCore.Migrations;

namespace TastyBoutique.Persistance.Migrations
{
    public partial class databaserecipecomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeComment",
                table: "RecipeComment");

            migrationBuilder.RenameTable(
                name: "RecipeComment",
                newName: "RecipeComments");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeComment_IdUser",
                table: "RecipeComments",
                newName: "IX_RecipeComments_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeComment_IdRecipe",
                table: "RecipeComments",
                newName: "IX_RecipeComments_IdRecipe");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeComments",
                table: "RecipeComments",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeComments",
                table: "RecipeComments");

            migrationBuilder.RenameTable(
                name: "RecipeComments",
                newName: "RecipeComment");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeComments_IdUser",
                table: "RecipeComment",
                newName: "IX_RecipeComment_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeComments_IdRecipe",
                table: "RecipeComment",
                newName: "IX_RecipeComment_IdRecipe");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeComment",
                table: "RecipeComment",
                column: "Id");
        }
    }
}
