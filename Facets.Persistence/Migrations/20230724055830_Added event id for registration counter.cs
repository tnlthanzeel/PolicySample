using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addedeventidforregistrationcounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "RegistrationCounter",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCounter_EventId",
                table: "RegistrationCounter",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationCounter_Event_EventId",
                table: "RegistrationCounter",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationCounter_Event_EventId",
                table: "RegistrationCounter");

            migrationBuilder.DropIndex(
                name: "IX_RegistrationCounter_EventId",
                table: "RegistrationCounter");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "RegistrationCounter");
        }
    }
}
