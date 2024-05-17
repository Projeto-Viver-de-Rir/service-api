using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionsForScheduleEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Occurence",
                table: "ScheduleEvents",
                newName: "Occurrence");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Schedule",
                table: "ScheduleEvents",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Schedule",
                table: "ScheduleEvents");

            migrationBuilder.RenameColumn(
                name: "Occurrence",
                table: "ScheduleEvents",
                newName: "Occurence");
        }
    }
}
