using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegDelta.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    ChildSessionId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Sessions_ChildSessionId",
                        column: x => x.ChildSessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stints",
                columns: table => new
                {
                    StintID = table.Column<string>(type: "TEXT", nullable: false),
                    FastestLapID = table.Column<string>(type: "TEXT", nullable: false),
                    SessionId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stints", x => x.StintID);
                    table.ForeignKey(
                        name: "FK_Stints_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Laps",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    LapTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    LapNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    StintID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Laps_Stints_StintID",
                        column: x => x.StintID,
                        principalTable: "Stints",
                        principalColumn: "StintID");
                });

            migrationBuilder.CreateTable(
                name: "PositionPoints",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    X = table.Column<double>(type: "REAL", nullable: false),
                    Y = table.Column<double>(type: "REAL", nullable: false),
                    Z = table.Column<double>(type: "REAL", nullable: false),
                    LapId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PositionPoints_Laps_LapId",
                        column: x => x.LapId,
                        principalTable: "Laps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sector",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SectorNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    SectorTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    LapId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sector", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sector_Laps_LapId",
                        column: x => x.LapId,
                        principalTable: "Laps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TelemetryPoints",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ThrottlePosition = table.Column<double>(type: "REAL", nullable: false),
                    BrakePosition = table.Column<double>(type: "REAL", nullable: false),
                    SteeringAngle = table.Column<double>(type: "REAL", nullable: false),
                    LapId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelemetryPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelemetryPoints_Laps_LapId",
                        column: x => x.LapId,
                        principalTable: "Laps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Laps_StintID",
                table: "Laps",
                column: "StintID");

            migrationBuilder.CreateIndex(
                name: "IX_PositionPoints_LapId",
                table: "PositionPoints",
                column: "LapId");

            migrationBuilder.CreateIndex(
                name: "IX_Sector_LapId",
                table: "Sector",
                column: "LapId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ChildSessionId",
                table: "Sessions",
                column: "ChildSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Stints_SessionId",
                table: "Stints",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryPoints_LapId",
                table: "TelemetryPoints",
                column: "LapId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PositionPoints");

            migrationBuilder.DropTable(
                name: "Sector");

            migrationBuilder.DropTable(
                name: "TelemetryPoints");

            migrationBuilder.DropTable(
                name: "Laps");

            migrationBuilder.DropTable(
                name: "Stints");

            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
