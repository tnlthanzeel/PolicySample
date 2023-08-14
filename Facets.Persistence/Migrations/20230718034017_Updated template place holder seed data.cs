using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Updatedtemplateplaceholderseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "TemplatePlaceHolder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("01682e6d-a927-4737-af7d-6d7a24b892af"),
                column: "Token",
                value: "#PassGeneratedDate&Time#");

            migrationBuilder.UpdateData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("07ba8b87-54ca-40b3-b63e-1de8eb76ea8a"),
                column: "Token",
                value: "#PassDate#");

            migrationBuilder.UpdateData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("33d753ba-4083-4d02-8160-69caca42bde6"),
                column: "Token",
                value: "#EventName#");

            migrationBuilder.UpdateData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("37ab3da2-a533-4c8d-ac10-765455324861"),
                column: "Token",
                value: "#LastName#");

            migrationBuilder.UpdateData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("4487c879-3837-47f1-906c-2620a71a4622"),
                column: "Token",
                value: "#PassCategory#");

            migrationBuilder.UpdateData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("48032361-473f-4c78-aa63-b5e0ef240746"),
                column: "Token",
                value: "#NICNumberPassportNumber#");

            migrationBuilder.UpdateData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("774099f5-2421-4400-abac-715b65f491b8"),
                column: "Token",
                value: "#ProfileImage#");

            migrationBuilder.UpdateData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("af2e3eff-4969-49cb-b872-0b3958785df3"),
                column: "Token",
                value: "#MobileNumber#");

            migrationBuilder.UpdateData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("b7632b78-73c6-4e20-a8e0-889f71a7958a"),
                column: "Token",
                value: "#GeneratedQR#");

            migrationBuilder.UpdateData(
                table: "TemplatePlaceHolder",
                keyColumn: "Id",
                keyValue: new Guid("f7789f28-5d94-4cb5-877b-0e91c4fda2e8"),
                column: "Token",
                value: "#FirstName#");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "TemplatePlaceHolder");
        }
    }
}
