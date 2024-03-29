using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Well_Up_API.Migrations
{
    /// <inheritdoc />
    public partial class ActiveChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "HabitLog");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "UserHabit",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "UserHabit");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "HabitLog",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
