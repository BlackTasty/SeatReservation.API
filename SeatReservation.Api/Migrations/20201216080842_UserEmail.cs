using Microsoft.EntityFrameworkCore.Migrations;

namespace SeatReservation.Api.Migrations
{
    public partial class UserEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "users");
        }
    }
}
