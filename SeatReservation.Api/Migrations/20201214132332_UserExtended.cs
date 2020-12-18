using Microsoft.EntityFrameworkCore.Migrations;

namespace SeatReservation.Api.Migrations
{
    public partial class UserExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                table: "users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "users");

            migrationBuilder.DropColumn(
                name: "State",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
