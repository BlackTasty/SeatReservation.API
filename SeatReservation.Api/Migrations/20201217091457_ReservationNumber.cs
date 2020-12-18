using Microsoft.EntityFrameworkCore.Migrations;

namespace SeatReservation.Api.Migrations
{
    public partial class ReservationNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "ReservationNumber",
                table: "Reservations",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationNumber",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
