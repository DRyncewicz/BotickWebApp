using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BotickAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OnModelCreatingConfigurationChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
