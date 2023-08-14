using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Adddefaultpasscategorysettingsforseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PassCategorySettings",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ForeignerDiscountAmount", "ForeignerRate", "IsChargeable", "LocalDiscountAmount", "LocalRate", "PassCategoryId", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("70021f76-eb22-484a-a66c-f4ffcea474f8"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0m, 0m, false, 0m, 0m, new Guid("b435dc73-7688-44e1-af8e-d00743b6a2e6"), null, null },
                    { new Guid("d06f02f4-50e0-4b9f-9a4b-c4a4cab10aee"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0m, 0m, false, 0m, 0m, new Guid("9dee9062-8d9e-469c-b9fb-0770348fe4c4"), null, null },
                    { new Guid("f42ef064-0b4e-4600-a6b6-d999d364c74d"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0m, 0m, false, 0m, 0m, new Guid("cdbf712c-5997-40b0-b281-a01a8d592aab"), null, null },
                    { new Guid("fdc613a3-061d-47fa-bc19-7e179caff73f"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0m, 0m, false, 0m, 0m, new Guid("a7bd59a2-2e3a-4d25-9f7e-7e8fafa174a3"), null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PassCategorySettings",
                keyColumn: "Id",
                keyValue: new Guid("70021f76-eb22-484a-a66c-f4ffcea474f8"));

            migrationBuilder.DeleteData(
                table: "PassCategorySettings",
                keyColumn: "Id",
                keyValue: new Guid("d06f02f4-50e0-4b9f-9a4b-c4a4cab10aee"));

            migrationBuilder.DeleteData(
                table: "PassCategorySettings",
                keyColumn: "Id",
                keyValue: new Guid("f42ef064-0b4e-4600-a6b6-d999d364c74d"));

            migrationBuilder.DeleteData(
                table: "PassCategorySettings",
                keyColumn: "Id",
                keyValue: new Guid("fdc613a3-061d-47fa-bc19-7e179caff73f"));
        }
    }
}
