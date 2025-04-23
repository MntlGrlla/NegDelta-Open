using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegDelta.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSectorTimesColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sector_Laps_LapId",
                table: "Sector");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sector",
                table: "Sector");

            migrationBuilder.RenameTable(
                name: "Sector",
                newName: "SectorTimes");

            migrationBuilder.RenameIndex(
                name: "IX_Sector_LapId",
                table: "SectorTimes",
                newName: "IX_SectorTimes_LapId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectorTimes",
                table: "SectorTimes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SectorTimes_Laps_LapId",
                table: "SectorTimes",
                column: "LapId",
                principalTable: "Laps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectorTimes_Laps_LapId",
                table: "SectorTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectorTimes",
                table: "SectorTimes");

            migrationBuilder.RenameTable(
                name: "SectorTimes",
                newName: "Sector");

            migrationBuilder.RenameIndex(
                name: "IX_SectorTimes_LapId",
                table: "Sector",
                newName: "IX_Sector_LapId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sector",
                table: "Sector",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sector_Laps_LapId",
                table: "Sector",
                column: "LapId",
                principalTable: "Laps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
