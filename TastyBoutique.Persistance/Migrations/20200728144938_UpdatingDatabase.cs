using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TastyBoutique.Persistance.Migrations
{
    public partial class UpdatingDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Access = table.Column<string>(maxLength: 25, nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    Image = table.Column<byte[]>(type: "image", nullable: true),
                    Link = table.Column<string>(maxLength: 150, nullable: true),
                    Notifications = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Age = table.Column<decimal>(type: "numeric(2, 0)", nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdRecipe = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Recipes",
                        column: x => x.IdRecipe,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipesFilters",
                columns: table => new
                {
                    RecipeId = table.Column<Guid>(nullable: false),
                    FilterId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipesFilters", x => new { x.RecipeId, x.FilterId });
                    table.ForeignKey(
                        name: "FK_RecipesFilters_Filters",
                        column: x => x.FilterId,
                        principalTable: "Filters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipesFilters_Recipes",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipesIngredients",
                columns: table => new
                {
                    RecipeId = table.Column<Guid>(nullable: false),
                    IngredientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipesIngredients", x => new { x.RecipeId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_RecipesIngredients_Ingredients",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipesIngredients_Recipes",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeType",
                columns: table => new
                {
                    RecipeID = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeType", x => x.RecipeID);
                    table.ForeignKey(
                        name: "FK_RecipeType_Recipes",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdStudent = table.Column<Guid>(nullable: false),
                    IdUserType = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Student",
                        column: x => x.IdStudent,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_UserType",
                        column: x => x.IdUserType,
                        principalTable: "UserType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdRecipe = table.Column<Guid>(nullable: false),
                    IdUser = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(maxLength: 250, nullable: false),
                    Review = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeComment_Recipes",
                        column: x => x.IdRecipe,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeComment_User",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SavedRecipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdRecipe = table.Column<Guid>(nullable: false),
                    IdUser = table.Column<Guid>(nullable: false),
                    NeedUpdate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedRecipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedRecipes_Recipes",
                        column: x => x.IdRecipe,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedRecipes_User",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_IdRecipe",
                table: "Notifications",
                column: "IdRecipe");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeComment_IdRecipe",
                table: "RecipeComment",
                column: "IdRecipe");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeComment_IdUser",
                table: "RecipeComment",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_RecipesFilters_FilterId",
                table: "RecipesFilters",
                column: "FilterId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipesFilters_RecipeId",
                table: "RecipesFilters",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipesIngredients_IngredientId",
                table: "RecipesIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipesIngredients_RecipeId",
                table: "RecipesIngredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeType_RecipeID",
                table: "RecipeType",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_SavedRecipes_IdRecipe",
                table: "SavedRecipes",
                column: "IdRecipe");

            migrationBuilder.CreateIndex(
                name: "IX_SavedRecipes_IdUser",
                table: "SavedRecipes",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdStudent",
                table: "User",
                column: "IdStudent");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdUserType",
                table: "User",
                column: "IdUserType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "RecipeComment");

            migrationBuilder.DropTable(
                name: "RecipesFilters");

            migrationBuilder.DropTable(
                name: "RecipesIngredients");

            migrationBuilder.DropTable(
                name: "RecipeType");

            migrationBuilder.DropTable(
                name: "SavedRecipes");

            migrationBuilder.DropTable(
                name: "Filters");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}
