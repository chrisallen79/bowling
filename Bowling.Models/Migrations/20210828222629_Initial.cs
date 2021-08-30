using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bowling.Models.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameStarted = table.Column<DateTime>(type: "TEXT", nullable: true, defaultValueSql: "date('now')"),
                    LastFrame = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Frames",
                columns: table => new
                {
                    FrameId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FrameSequence = table.Column<int>(type: "INTEGER", nullable: false),
                    Roll1 = table.Column<int>(type: "INTEGER", nullable: true),
                    Roll2 = table.Column<int>(type: "INTEGER", nullable: true),
                    Roll3 = table.Column<int>(type: "INTEGER", nullable: true),
                    GameId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frames", x => x.FrameId);
                    table.ForeignKey(
                        name: "FK_Frames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "LastFrame" },
                values: new object[] { 1, 0 });

            migrationBuilder.InsertData(
                table: "Frames",
                columns: new[] { "FrameId", "FrameSequence", "GameId", "Roll1", "Roll2", "Roll3" },
                values: new object[] { 1, 0, 1, 10, null, null });

            migrationBuilder.InsertData(
                table: "Frames",
                columns: new[] { "FrameId", "FrameSequence", "GameId", "Roll1", "Roll2", "Roll3" },
                values: new object[] { 2, 0, 1, 10, null, null });

            migrationBuilder.InsertData(
                table: "Frames",
                columns: new[] { "FrameId", "FrameSequence", "GameId", "Roll1", "Roll2", "Roll3" },
                values: new object[] { 3, 0, 1, 10, null, null });

            migrationBuilder.InsertData(
                table: "Frames",
                columns: new[] { "FrameId", "FrameSequence", "GameId", "Roll1", "Roll2", "Roll3" },
                values: new object[] { 4, 0, 1, 10, null, null });

            migrationBuilder.InsertData(
                table: "Frames",
                columns: new[] { "FrameId", "FrameSequence", "GameId", "Roll1", "Roll2", "Roll3" },
                values: new object[] { 5, 0, 1, 10, null, null });

            migrationBuilder.InsertData(
                table: "Frames",
                columns: new[] { "FrameId", "FrameSequence", "GameId", "Roll1", "Roll2", "Roll3" },
                values: new object[] { 6, 0, 1, 10, null, null });

            migrationBuilder.InsertData(
                table: "Frames",
                columns: new[] { "FrameId", "FrameSequence", "GameId", "Roll1", "Roll2", "Roll3" },
                values: new object[] { 7, 0, 1, 10, null, null });

            migrationBuilder.InsertData(
                table: "Frames",
                columns: new[] { "FrameId", "FrameSequence", "GameId", "Roll1", "Roll2", "Roll3" },
                values: new object[] { 8, 0, 1, 10, null, null });

            migrationBuilder.InsertData(
                table: "Frames",
                columns: new[] { "FrameId", "FrameSequence", "GameId", "Roll1", "Roll2", "Roll3" },
                values: new object[] { 9, 0, 1, 10, null, null });

            migrationBuilder.InsertData(
                table: "Frames",
                columns: new[] { "FrameId", "FrameSequence", "GameId", "Roll1", "Roll2", "Roll3" },
                values: new object[] { 10, 0, 1, 10, 10, 10 });

            migrationBuilder.CreateIndex(
                name: "IX_Frames_GameId",
                table: "Frames",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Frames");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
