using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TastyBoutique.Persistance.Migrations
{
    public partial class UserIdinRecipess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_User_IdUserNavigationId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_IdUserNavigationId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IdUserNavigationId",
                table: "Recipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdUserNavigationId",
                table: "Recipes",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
