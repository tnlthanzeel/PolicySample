using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addedtemplateplaceholderseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TemplatePlaceHolder",
                columns: new[] { "Id", "DisplayName" },
                values: new object[,]
                {
                    { new Guid("01682e6d-a927-4737-af7d-6d7a24b892af"), "Pass Generated Date & Time" },
                    { new Guid("07ba8b87-54ca-40b3-b63e-1de8eb76ea8a"), "Pass Date" },
                    { new Guid("33d753ba-4083-4d02-8160-69caca42bde6"), "Event Name" },
                    { new Guid("37ab3da2-a533-4c8d-ac10-765455324861"), "Last Name" },
                    { new Guid("4487c879-3837-47f1-906c-2620a71a4622"), "Pass Category" },
                    { new Guid("48032361-473f-4c78-aa63-b5e0ef240746"), "NIC Number/ Passport Number" },
                    { new Guid("774099f5-2421-4400-abac-715b65f491b8"), "Profile Image" },
                    { new Guid("af2e3eff-4969-49cb-b872-0b3958785df3"), "Mobile Number" },
                    { new Guid("b7632b78-73c6-4e20-a8e0-889f71a7958a"), "Generated QR" },
                    { new Guid("f7789f28-5d94-4cb5-877b-0e91c4fda2e8"), "First Name" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("01682e6d-a927-4737-af7d-6d7a24b892af"));

            migrationBuilder.DeleteData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("07ba8b87-54ca-40b3-b63e-1de8eb76ea8a"));

            migrationBuilder.DeleteData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("33d753ba-4083-4d02-8160-69caca42bde6"));

            migrationBuilder.DeleteData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("37ab3da2-a533-4c8d-ac10-765455324861"));

            migrationBuilder.DeleteData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("4487c879-3837-47f1-906c-2620a71a4622"));

            migrationBuilder.DeleteData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("48032361-473f-4c78-aa63-b5e0ef240746"));

            migrationBuilder.DeleteData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("774099f5-2421-4400-abac-715b65f491b8"));

            migrationBuilder.DeleteData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("af2e3eff-4969-49cb-b872-0b3958785df3"));

            migrationBuilder.DeleteData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("b7632b78-73c6-4e20-a8e0-889f71a7958a"));

            migrationBuilder.DeleteData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("f7789f28-5d94-4cb5-877b-0e91c4fda2e8"));
        }
    }
}
