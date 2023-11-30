using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Well_Up_API.Migrations
{
    /// <inheritdoc />
    public partial class userhabitfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHabit_Habit_UserHabitId",
                table: "UserHabit");

            migrationBuilder.AlterColumn<int>(
                name: "UserHabitId",
                table: "UserHabit",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_UserHabit_HabitId",
                table: "UserHabit",
                column: "HabitId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserHabit_Habit_HabitId",
                table: "UserHabit",
                column: "HabitId",
                principalTable: "Habit",
                principalColumn: "HabitId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHabit_Habit_HabitId",
                table: "UserHabit");

            migrationBuilder.DropIndex(
                name: "IX_UserHabit_HabitId",
                table: "UserHabit");

            migrationBuilder.AlterColumn<int>(
                name: "UserHabitId",
                table: "UserHabit",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHabit_Habit_UserHabitId",
                table: "UserHabit",
                column: "UserHabitId",
                principalTable: "Habit",
                principalColumn: "HabitId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
