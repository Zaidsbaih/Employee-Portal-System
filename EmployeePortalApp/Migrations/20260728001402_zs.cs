using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePortalApp.Migrations
{
    /// <inheritdoc />
    public partial class zs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordSetupToken",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordSetupTokenExpiry",
                table: "Employees",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSetupToken",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PasswordSetupTokenExpiry",
                table: "Employees");
        }
    }
}
