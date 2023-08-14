using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MadenameandeventidUniqueconstrainpasscategorytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PassCategory_Name",
                table: "PassCategory");

            migrationBuilder.CreateIndex(
                name: "IX_PassCategory_Name_EventId",
                table: "PassCategory",
                columns: new[] { "Name", "EventId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PassCategory_Name_EventId",
                table: "PassCategory");

            migrationBuilder.CreateIndex(
                name: "IX_PassCategory_Name",
                table: "PassCategory",
                column: "Name",
                unique: true);
        }
    }
}
