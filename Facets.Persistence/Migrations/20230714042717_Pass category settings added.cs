using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Passcategorysettingsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PassCategorySettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PassCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsChargeable = table.Column<bool>(type: "bit", nullable: false),
                    LocalRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LocalDiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ForeignerRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ForeignerDiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassCategorySettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassCategorySettings_PassCategory_PassCategoryId",
                        column: x => x.PassCategoryId,
                        principalTable: "PassCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassCategorySettings_PassCategoryId",
                table: "PassCategorySettings",
                column: "PassCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassCategorySettings");
        }
    }
}
