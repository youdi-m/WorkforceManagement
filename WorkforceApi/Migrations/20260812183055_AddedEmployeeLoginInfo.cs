using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkforceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmployeeLoginInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "passwordHash",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "Employees");
        }
    }
}
