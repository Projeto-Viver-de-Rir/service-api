using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Institutional.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TeamMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    VolunteerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamMembers_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_VolunteerId",
                table: "TeamMembers",
                column: "VolunteerId");
            
            // Roles Base
            migrationBuilder.InsertData(table: "AspNetRoles", 
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new[] { "f008d5a7-4e30-4848-a185-977d24391c8c", "Fiscal", "FISCAL", "d137d8e2-bf10-4f89-81ae-a720566e2489" });
            
            migrationBuilder.InsertData(table: "AspNetRoles", 
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new[] { "087ffdc6-c81a-41ba-9b5a-7803f9c9baf4", "Advisory", "ADVISORY", "bbf7e0dc-7705-471f-a686-16ed8fb1a1c1" });            
            
            migrationBuilder.InsertData(table: "AspNetRoles", 
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new[] { "5a1e47e3-8dc9-4f04-bd7b-e1361ffdf6b3", "Legal", "LEGAL", "b0af3a11-00b6-4be0-ba05-2da3a6bc7c12" });
            
            migrationBuilder.InsertData(table: "AspNetRoles", 
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new[] { "ddaf7f36-e322-4914-8697-b5f17b67c544", "Operational", "OPERATIONAL", "6b3f37d6-faf1-41e2-8abb-b5b5ee34fd50" });
            
            migrationBuilder.InsertData(table: "AspNetRoles", 
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new[] { "46aa2634-5c6d-425d-891e-35f5343e2331", "Administrative", "ADMINISTRATIVE", "61f8381b-1147-43a6-8e51-e85454253cfd" });            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamMembers");
            
            // Roles Base
            migrationBuilder.DeleteData(table: "TeamMembers", 
                keyColumn: "Id",
                keyValue: "f008d5a7-4e30-4848-a185-977d24391c8c");
            
            migrationBuilder.DeleteData(table: "TeamMembers", 
                keyColumn: "Id",
                keyValue: "087ffdc6-c81a-41ba-9b5a-7803f9c9baf4");
            
            migrationBuilder.DeleteData(table: "TeamMembers", 
                keyColumn: "Id",
                keyValue: "5a1e47e3-8dc9-4f04-bd7b-e1361ffdf6b3");
            
            migrationBuilder.DeleteData(table: "TeamMembers", 
                keyColumn: "Id",
                keyValue: "ddaf7f36-e322-4914-8697-b5f17b67c544");
            
            migrationBuilder.DeleteData(table: "TeamMembers", 
                keyColumn: "Id",
                keyValue: "46aa2634-5c6d-425d-891e-35f5343e2331");            
        }
    }
}
