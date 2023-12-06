using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Well_Up_API.Migrations
{
    /// <inheritdoc />
    public partial class UserHabitCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserHabit",
                table: "UserHabit");

            migrationBuilder.AlterColumn<int>(
                name: "UserHabitId",
                table: "UserHabit",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserHabit",
                table: "UserHabit",
                columns: new[] { "UserHabitId", "UserId", "HabitId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserHabit",
                table: "UserHabit");

            migrationBuilder.AlterColumn<int>(
                name: "UserHabitId",
                table: "UserHabit",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserHabit",
                table: "UserHabit",
                column: "UserHabitId");
        }
    }
}
