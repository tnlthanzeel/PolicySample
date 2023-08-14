﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsLockedToVisitorRegistationCounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "VisitorRegistrationCounter",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "VisitorRegistrationCounter");
        }
    }
}
