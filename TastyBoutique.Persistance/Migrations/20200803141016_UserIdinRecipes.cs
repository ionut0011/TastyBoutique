using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TastyBoutique.Persistance.Migrations
{
    public partial class UserIdinRecipes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdUser",
                table: "Recipes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdUserNavigationId",
                table: "Recipes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_IdUser",
                table: "Recipes",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_IdUserNavigationId",
                table: "Recipes",
                column: "IdUserNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_User_IdUserNavigationId",
                table: "Recipes",
                column: "IdUserNavigationId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_User_IdUserNavigationId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_IdUser",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_IdUserNavigationId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IdUserNavigationId",
                table: "Recipes");
        }
    }
}
