using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TastyBoutique.Persistance.Migrations
{
    public partial class updaterecipes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<Guid>(
                name: "RecipesId",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RecipesId",
                table: "Filters",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipesId",
                table: "Ingredients",
                column: "RecipesId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_RecipesId",
                table: "Filters",
                column: "RecipesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filters_Recipes_RecipesId",
                table: "Filters",
                column: "RecipesId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Recipes_RecipesId",
                table: "Ingredients",
                column: "RecipesId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filters_Recipes_RecipesId",
                table: "Filters");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_RecipesId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_RecipesId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Filters_RecipesId",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "RecipesId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "RecipesId",
                table: "Filters");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    IdRecipe = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Notifications_Recipes",
                        column: x => x.IdRecipe,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });
        }
    }
}
