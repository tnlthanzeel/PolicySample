using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addedauditforuserprofiletable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UserProfile",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "UserProfile",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "UserProfile",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOn",
                table: "UserProfile",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "UserProfile",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedOn",
                table: "UserProfile",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsDeleted", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTimeOffset(new DateTime(2022, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 30, 0, 0)), null, null, false, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_IsDeleted",
                table: "UserProfile",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfile_IsDeleted",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "UserProfile");
        }
    }
}
