using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkforceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeShifts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Employees",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "ShiftEndTime",
                table: "Employees",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "ShiftStartTime",
                table: "Employees",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ShiftEndTime",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ShiftStartTime",
                table: "Employees");
        }
    }
}
