using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Madedisplaynameandtokenunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "TemplatePlaceHolder",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TemplatePlaceHolder_DisplayName",
                table: "TemplatePlaceHolder",
                column: "DisplayName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TemplatePlaceHolder_Token",
                table: "TemplatePlaceHolder",
                column: "Token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TemplatePlaceHolder_DisplayName",
                table: "TemplatePlaceHolder");

            migrationBuilder.DropIndex(
                name: "IX_TemplatePlaceHolder_Token",
                table: "TemplatePlaceHolder");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "TemplatePlaceHolder",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
