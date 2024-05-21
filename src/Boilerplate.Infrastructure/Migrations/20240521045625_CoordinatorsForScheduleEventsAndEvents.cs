using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CoordinatorsForScheduleEventsAndEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Volunteers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventCoordinators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    VolunteerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCoordinators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventCoordinators_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventCoordinators_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleEventCoordinators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ScheduleEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    VolunteerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleEventCoordinators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleEventCoordinators_ScheduleEvents_ScheduleEventId",
                        column: x => x.ScheduleEventId,
                        principalTable: "ScheduleEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleEventCoordinators_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventPresences_EventId",
                table: "EventPresences",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventPresences_VolunteerId",
                table: "EventPresences",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_EventCoordinators_EventId",
                table: "EventCoordinators",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventCoordinators_VolunteerId",
                table: "EventCoordinators",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEventCoordinators_ScheduleEventId",
                table: "ScheduleEventCoordinators",
                column: "ScheduleEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEventCoordinators_VolunteerId",
                table: "ScheduleEventCoordinators",
                column: "VolunteerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventPresences_Events_EventId",
                table: "EventPresences",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventPresences_Volunteers_VolunteerId",
                table: "EventPresences",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventPresences_Events_EventId",
                table: "EventPresences");

            migrationBuilder.DropForeignKey(
                name: "FK_EventPresences_Volunteers_VolunteerId",
                table: "EventPresences");

            migrationBuilder.DropTable(
                name: "EventCoordinators");

            migrationBuilder.DropTable(
                name: "ScheduleEventCoordinators");

            migrationBuilder.DropIndex(
                name: "IX_EventPresences_EventId",
                table: "EventPresences");

            migrationBuilder.DropIndex(
                name: "IX_EventPresences_VolunteerId",
                table: "EventPresences");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Volunteers");
        }
    }
}
