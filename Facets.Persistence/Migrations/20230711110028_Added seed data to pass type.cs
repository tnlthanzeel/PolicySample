using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addedseeddatatopasstype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PassType",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsDefault", "IsDeleted", "Name", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("53605f0f-4309-445d-a1f6-962567aa5af2"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, "Team Member", null, null },
                    { new Guid("8916d851-6dd4-4592-99c0-4a45e46fb269"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, "Visitor", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PassType",
                keyColumn: "Id",
                keyValue: new Guid("53605f0f-4309-445d-a1f6-962567aa5af2"));

            migrationBuilder.DeleteData(
                table: "PassType",
                keyColumn: "Id",
                keyValue: new Guid("8916d851-6dd4-4592-99c0-4a45e46fb269"));
        }
    }
}
