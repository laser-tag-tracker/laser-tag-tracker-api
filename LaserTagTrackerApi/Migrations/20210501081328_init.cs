using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LaserTagTrackerApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerName = table.Column<string>(type: "text", nullable: true),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    Precision = table.Column<double>(type: "double precision", nullable: false),
                    TeamScore = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ChestGiven = table.Column<int>(type: "integer", nullable: false),
                    BackGiven = table.Column<int>(type: "integer", nullable: false),
                    ShouldersGiven = table.Column<int>(type: "integer", nullable: false),
                    GunGiven = table.Column<int>(type: "integer", nullable: false),
                    ChestReceived = table.Column<int>(type: "integer", nullable: false),
                    BackReceived = table.Column<int>(type: "integer", nullable: false),
                    ShouldersReceived = table.Column<int>(type: "integer", nullable: false),
                    GunReceived = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UserId",
                table: "Matches",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
