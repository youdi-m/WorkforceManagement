using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkforceApi.Migrations
{
    /// <inheritdoc />
    public partial class FixEmployeeFieldNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role",
                table: "Employees",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "passwordHash",
                table: "Employees",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Employees",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Employees",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Employees",
                newName: "passwordHash");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Employees",
                newName: "email");
        }
    }
}
