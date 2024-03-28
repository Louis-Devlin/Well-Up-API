using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Well_Up_API.Migrations
{
    /// <inheritdoc />
    public partial class habitlogactivechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "HabitLog",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "HabitLog");
        }
    }
}
