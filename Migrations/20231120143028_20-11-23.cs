using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Well_Up_API.Migrations
{
    /// <inheritdoc />
    public partial class _201123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Colour",
                table: "Mood",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Colour",
                table: "Mood");
        }
    }
}
