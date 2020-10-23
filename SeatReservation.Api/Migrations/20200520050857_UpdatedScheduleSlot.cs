using Microsoft.EntityFrameworkCore.Migrations;

namespace SeatReservation.Api.Migrations
{
    public partial class UpdatedScheduleSlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schedule_slots_movies_MovieId",
                table: "schedule_slots");

            migrationBuilder.DropIndex(
                name: "IX_schedule_slots_MovieId",
                table: "schedule_slots");

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "schedule_slots",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "schedule_slots");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_slots_MovieId",
                table: "schedule_slots",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_schedule_slots_movies_MovieId",
                table: "schedule_slots",
                column: "MovieId",
                principalTable: "movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
