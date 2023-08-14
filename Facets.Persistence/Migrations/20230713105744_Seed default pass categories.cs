using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Seeddefaultpasscategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PassCategory",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "Name", "PassTypeId", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("9dee9062-8d9e-469c-b9fb-0770348fe4c4"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Sri Lankan visitor", false, "Local - Visitor", new Guid("8916d851-6dd4-4592-99c0-4a45e46fb269"), null, null },
                    { new Guid("a7bd59a2-2e3a-4d25-9f7e-7e8fafa174a3"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Association Member", false, "Member", new Guid("8916d851-6dd4-4592-99c0-4a45e46fb269"), null, null },
                    { new Guid("b435dc73-7688-44e1-af8e-d00743b6a2e6"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Foreigner & Buyer", false, "Foreign - Buyer", new Guid("8916d851-6dd4-4592-99c0-4a45e46fb269"), null, null },
                    { new Guid("cdbf712c-5997-40b0-b281-a01a8d592aab"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Foreigner & Visitor", false, "Foreign - Visitor", new Guid("8916d851-6dd4-4592-99c0-4a45e46fb269"), null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PassCategory",
                keyColumn: "Id",
                keyValue: new Guid("9dee9062-8d9e-469c-b9fb-0770348fe4c4"));

            migrationBuilder.DeleteData(
                table: "PassCategory",
                keyColumn: "Id",
                keyValue: new Guid("a7bd59a2-2e3a-4d25-9f7e-7e8fafa174a3"));

            migrationBuilder.DeleteData(
                table: "PassCategory",
                keyColumn: "Id",
                keyValue: new Guid("b435dc73-7688-44e1-af8e-d00743b6a2e6"));

            migrationBuilder.DeleteData(
                table: "PassCategory",
                keyColumn: "Id",
                keyValue: new Guid("cdbf712c-5997-40b0-b281-a01a8d592aab"));
        }
    }
}
