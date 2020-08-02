using Microsoft.EntityFrameworkCore.Migrations;

namespace TastyBoutique.Persistance.Migrations
{
    public partial class updateReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Review",
                table: "RecipeComment",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Review",
                table: "RecipeComment",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldFixedLength: true,
                oldMaxLength: 10);
        }
    }
}
