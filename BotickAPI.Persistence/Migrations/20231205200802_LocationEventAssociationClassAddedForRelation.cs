using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BotickAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LocationEventAssociationClassAddedForRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Events_EventId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_EventId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Locations");

            migrationBuilder.CreateTable(
                name: "LocationEvent",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationEvent", x => new { x.EventId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_LocationEvent_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationEvent_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationEvent_LocationId",
                table: "LocationEvent",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationEvent");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Locations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_EventId",
                table: "Locations",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Events_EventId",
                table: "Locations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
