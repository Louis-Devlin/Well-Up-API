using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Well_Up_API.Migrations
{
    /// <inheritdoc />
    public partial class habbit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HabitLog_Habbit_HabbitId",
                table: "HabitLog");

            migrationBuilder.DropTable(
                name: "Habbit");

            migrationBuilder.RenameColumn(
                name: "HabbitId",
                table: "HabitLog",
                newName: "HabitId");

            migrationBuilder.RenameColumn(
                name: "HabbitLogId",
                table: "HabitLog",
                newName: "HabitLogId");

            migrationBuilder.RenameIndex(
                name: "IX_HabitLog_HabbitId",
                table: "HabitLog",
                newName: "IX_HabitLog_HabitId");

            migrationBuilder.CreateTable(
                name: "Habit",
                columns: table => new
                {
                    HabitId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HabitName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habit", x => x.HabitId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_HabitLog_Habit_HabitId",
                table: "HabitLog",
                column: "HabitId",
                principalTable: "Habit",
                principalColumn: "HabitId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HabitLog_Habit_HabitId",
                table: "HabitLog");

            migrationBuilder.DropTable(
                name: "Habit");

            migrationBuilder.RenameColumn(
                name: "HabitId",
                table: "HabitLog",
                newName: "HabbitId");

            migrationBuilder.RenameColumn(
                name: "HabitLogId",
                table: "HabitLog",
                newName: "HabbitLogId");

            migrationBuilder.RenameIndex(
                name: "IX_HabitLog_HabitId",
                table: "HabitLog",
                newName: "IX_HabitLog_HabbitId");

            migrationBuilder.CreateTable(
                name: "Habbit",
                columns: table => new
                {
                    HabbitId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HabbitName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habbit", x => x.HabbitId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_HabitLog_Habbit_HabbitId",
                table: "HabitLog",
                column: "HabbitId",
                principalTable: "Habbit",
                principalColumn: "HabbitId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
