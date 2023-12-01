using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BotickAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AuthenticationRecordsModifyRecording : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                table: "Events",
                newName: "OrganizerEmail");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "EventReviews",
                newName: "UserEmail");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Bookings",
                newName: "UserEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrganizerEmail",
                table: "Events",
                newName: "OrganizerId");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "EventReviews",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Bookings",
                newName: "UserId");
        }
    }
}
