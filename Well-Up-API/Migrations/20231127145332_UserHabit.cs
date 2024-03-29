using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Well_Up_API.Migrations
{
    /// <inheritdoc />
    public partial class UserHabit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserHabit",
                columns: table => new
                {
                    UserHabitId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    HabitId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHabit", x => x.UserHabitId);
                    table.ForeignKey(
                        name: "FK_UserHabit_Habit_UserHabitId",
                        column: x => x.UserHabitId,
                        principalTable: "Habit",
                        principalColumn: "HabitId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserHabit_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserHabit_UserId",
                table: "UserHabit",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserHabit");
        }
    }
}
