using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Institutional.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MissingItemsForVolunteerAndCofig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Configs");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Volunteers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Volunteers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "Volunteers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Configs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            
            // Configuration Base
            migrationBuilder.InsertData(table: "Configs", 
                columns: new[] { "Id", "Type", "Description", "Value" },
                values: new[] { "edf35564-d046-41dd-8cda-ad1b536d72b8", "1", "Allow multiple events in the same day for one volunteer.", "true" });
            
            migrationBuilder.InsertData(table: "Configs", 
                columns: new[] { "Id", "Type", "Description", "Value" },
                values: new[] { "ae9d1269-dda3-4aa6-9217-972d87b932d3", "2", "Specifies when a new volunteer could be individually created.", "{\"allowAt\": \"2024-05-01T00:00:00.000Z\", \"blockAfter\": \"2024-05-31T00:00:00.000Z\"}" });            
            
            migrationBuilder.InsertData(table: "Configs", 
                columns: new[] { "Id", "Type", "Description", "Value" },
                values: new[] { "3cb829da-241f-47f9-9013-cdee83d75f18", "3", "Default amount to generate new debts.", "15.00" });              
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Configs");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Configs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Configs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Configs",
                type: "text",
                nullable: false,
                defaultValue: "");
            
            // Configuration Base
            migrationBuilder.DeleteData(table: "Configs", 
                keyColumn: "Id",
                keyValue: "edf35564-d046-41dd-8cda-ad1b536d72b8");
            
            migrationBuilder.DeleteData(table: "Configs", 
                keyColumn: "Id",
                keyValue: "ae9d1269-dda3-4aa6-9217-972d87b932d3");
            
            migrationBuilder.DeleteData(table: "Configs", 
                keyColumn: "Id",
                keyValue: "3cb829da-241f-47f9-9013-cdee83d75f18");
        }
    }
}
