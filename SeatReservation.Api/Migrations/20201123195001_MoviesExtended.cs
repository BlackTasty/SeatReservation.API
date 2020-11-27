using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SeatReservation.Api.Migrations
{
    public partial class MoviesExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Actors",
                table: "movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Directors",
                table: "movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Studios",
                table: "movies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "people",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "studios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "people");

            migrationBuilder.DropTable(
                name: "studios");

            migrationBuilder.DropColumn(
                name: "Actors",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "Directors",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "Studios",
                table: "movies");
        }
    }
}
