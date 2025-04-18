using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegDelta.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAllModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Sessions_ChildSessionId",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "ChildSessionId",
                table: "Sessions",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_ChildSessionId",
                table: "Sessions",
                newName: "IX_Sessions_SessionId");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FastestLapTime",
                table: "Stints",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "LapCount",
                table: "Stints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CarName",
                table: "Sessions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FastestLapTime",
                table: "Sessions",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "LapCount",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StintCount",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TrackName",
                table: "Sessions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Sessions_SessionId",
                table: "Sessions",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Sessions_SessionId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "FastestLapTime",
                table: "Stints");

            migrationBuilder.DropColumn(
                name: "LapCount",
                table: "Stints");

            migrationBuilder.DropColumn(
                name: "CarName",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "FastestLapTime",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "LapCount",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "StintCount",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "TrackName",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Sessions",
                newName: "ChildSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_SessionId",
                table: "Sessions",
                newName: "IX_Sessions_ChildSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Sessions_ChildSessionId",
                table: "Sessions",
                column: "ChildSessionId",
                principalTable: "Sessions",
                principalColumn: "Id");
        }
    }
}
