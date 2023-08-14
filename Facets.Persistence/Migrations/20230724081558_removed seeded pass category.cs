using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removedseededpasscategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrationCounter");

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

            migrationBuilder.CreateTable(
                name: "PassTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    HTMLText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisitorRegistrationCounter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorRegistrationCounter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitorRegistrationCounter_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassTemplate_IsDeleted",
                table: "PassTemplate",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorRegistrationCounter_EventId",
                table: "VisitorRegistrationCounter",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorRegistrationCounter_IsDeleted",
                table: "VisitorRegistrationCounter",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorRegistrationCounter_Name",
                table: "VisitorRegistrationCounter",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassTemplate");

            migrationBuilder.DropTable(
                name: "VisitorRegistrationCounter");

            migrationBuilder.CreateTable(
                name: "RegistrationCounter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationCounter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationCounter_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCounter_EventId",
                table: "RegistrationCounter",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCounter_IsDeleted",
                table: "RegistrationCounter",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCounter_Name",
                table: "RegistrationCounter",
                column: "Name",
                unique: true);
        }
    }
}
