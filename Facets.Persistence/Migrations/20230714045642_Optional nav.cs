using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Optionalnav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassCategorySettings_PassCategory_PassCategoryId",
                table: "PassCategorySettings");

            migrationBuilder.AddForeignKey(
                name: "FK_PassCategorySettings_PassCategory_PassCategoryId",
                table: "PassCategorySettings",
                column: "PassCategoryId",
                principalTable: "PassCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassCategorySettings_PassCategory_PassCategoryId",
                table: "PassCategorySettings");

            migrationBuilder.AddForeignKey(
                name: "FK_PassCategorySettings_PassCategory_PassCategoryId",
                table: "PassCategorySettings",
                column: "PassCategoryId",
                principalTable: "PassCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
