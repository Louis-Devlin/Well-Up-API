using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Well_Up_API.Migrations
{
    /// <inheritdoc />
    public partial class removeUserHabitComposite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserHabit",
                table: "UserHabit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserHabit",
                table: "UserHabit",
                column: "UserHabitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserHabit",
                table: "UserHabit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserHabit",
                table: "UserHabit",
                columns: new[] { "UserHabitId", "UserId", "HabitId" });
        }
    }
}
