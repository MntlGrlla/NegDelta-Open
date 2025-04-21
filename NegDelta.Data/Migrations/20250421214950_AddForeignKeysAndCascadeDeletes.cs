using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegDelta.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeysAndCascadeDeletes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laps_Stints_StintID",
                table: "Laps");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionPoints_Laps_LapId",
                table: "PositionPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_Sector_Laps_LapId",
                table: "Sector");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Sessions_SessionId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Stints_Sessions_SessionId",
                table: "Stints");

            migrationBuilder.DropForeignKey(
                name: "FK_TelemetryPoints_Laps_LapId",
                table: "TelemetryPoints");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_SessionId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "LapCount",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "StintCount",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "LapCount",
                table: "Stints",
                newName: "StintNumber");

            migrationBuilder.RenameColumn(
                name: "StintID",
                table: "Stints",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "StintID",
                table: "Laps",
                newName: "StintId");

            migrationBuilder.RenameIndex(
                name: "IX_Laps_StintID",
                table: "Laps",
                newName: "IX_Laps_StintId");

            migrationBuilder.AlterColumn<string>(
                name: "LapId",
                table: "TelemetryPoints",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SessionId",
                table: "Stints",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeCreated",
                table: "Sessions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "LapId",
                table: "Sector",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LapId",
                table: "PositionPoints",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StintId",
                table: "Laps",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Laps_Stints_StintId",
                table: "Laps",
                column: "StintId",
                principalTable: "Stints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionPoints_Laps_LapId",
                table: "PositionPoints",
                column: "LapId",
                principalTable: "Laps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sector_Laps_LapId",
                table: "Sector",
                column: "LapId",
                principalTable: "Laps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stints_Sessions_SessionId",
                table: "Stints",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TelemetryPoints_Laps_LapId",
                table: "TelemetryPoints",
                column: "LapId",
                principalTable: "Laps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laps_Stints_StintId",
                table: "Laps");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionPoints_Laps_LapId",
                table: "PositionPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_Sector_Laps_LapId",
                table: "Sector");

            migrationBuilder.DropForeignKey(
                name: "FK_Stints_Sessions_SessionId",
                table: "Stints");

            migrationBuilder.DropForeignKey(
                name: "FK_TelemetryPoints_Laps_LapId",
                table: "TelemetryPoints");

            migrationBuilder.DropColumn(
                name: "TimeCreated",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "StintNumber",
                table: "Stints",
                newName: "LapCount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Stints",
                newName: "StintID");

            migrationBuilder.RenameColumn(
                name: "StintId",
                table: "Laps",
                newName: "StintID");

            migrationBuilder.RenameIndex(
                name: "IX_Laps_StintId",
                table: "Laps",
                newName: "IX_Laps_StintID");

            migrationBuilder.AlterColumn<string>(
                name: "LapId",
                table: "TelemetryPoints",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "SessionId",
                table: "Stints",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "LapCount",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "Sessions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StintCount",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "LapId",
                table: "Sector",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "LapId",
                table: "PositionPoints",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "StintID",
                table: "Laps",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionId",
                table: "Sessions",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laps_Stints_StintID",
                table: "Laps",
                column: "StintID",
                principalTable: "Stints",
                principalColumn: "StintID");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionPoints_Laps_LapId",
                table: "PositionPoints",
                column: "LapId",
                principalTable: "Laps",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sector_Laps_LapId",
                table: "Sector",
                column: "LapId",
                principalTable: "Laps",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Sessions_SessionId",
                table: "Sessions",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stints_Sessions_SessionId",
                table: "Stints",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TelemetryPoints_Laps_LapId",
                table: "TelemetryPoints",
                column: "LapId",
                principalTable: "Laps",
                principalColumn: "Id");
        }
    }
}
