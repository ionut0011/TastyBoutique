using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TastyBoutique.Persistance.Migrations
{
    public partial class Table_Recipe_Field_Type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserType",
                table: "User");

            migrationBuilder.DropTable(
                name: "RecipeType");

            migrationBuilder.DropIndex(
                name: "IX_User_IdUserType",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IdUserType",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserTypeId",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Recipes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserTypeId",
                table: "User",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserType_UserTypeId",
                table: "User",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserType_UserTypeId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserTypeId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Recipes");

            migrationBuilder.AddColumn<Guid>(
                name: "IdUserType",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RecipeType",
                columns: table => new
                {
                    RecipeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_User_IdUserType",
                table: "User",
                column: "IdUserType");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeType_RecipeID",
                table: "RecipeType",
                column: "RecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserType",
                table: "User",
                column: "IdUserType",
                principalTable: "UserType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
