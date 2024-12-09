using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace resurec.Migrations
{
    /// <inheritdoc />
    public partial class dec08 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recordings_HardwareReportDTO_HardwareReportId",
                table: "Recordings");

            migrationBuilder.DropTable(
                name: "HardwareReportDTO");

            migrationBuilder.DropIndex(
                name: "IX_Recordings_HardwareReportId",
                table: "Recordings");

            migrationBuilder.DropColumn(
                name: "HardwareReportId",
                table: "Recordings");

            migrationBuilder.AddColumn<float>(
                name: "CpuTemperature",
                table: "Recordings",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "CpuUsage",
                table: "Recordings",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "GpuTemperature",
                table: "Recordings",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "GpuUsage",
                table: "Recordings",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "RamUsage",
                table: "Recordings",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CpuTemperature",
                table: "Recordings");

            migrationBuilder.DropColumn(
                name: "CpuUsage",
                table: "Recordings");

            migrationBuilder.DropColumn(
                name: "GpuTemperature",
                table: "Recordings");

            migrationBuilder.DropColumn(
                name: "GpuUsage",
                table: "Recordings");

            migrationBuilder.DropColumn(
                name: "RamUsage",
                table: "Recordings");

            migrationBuilder.AddColumn<Guid>(
                name: "HardwareReportId",
                table: "Recordings",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "HardwareReportDTO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CpuTemperature = table.Column<float>(type: "REAL", nullable: false),
                    CpuUsage = table.Column<float>(type: "REAL", nullable: false),
                    GpuTemperature = table.Column<float>(type: "REAL", nullable: false),
                    GpuUsage = table.Column<float>(type: "REAL", nullable: false),
                    RamUsage = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareReportDTO", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recordings_HardwareReportId",
                table: "Recordings",
                column: "HardwareReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recordings_HardwareReportDTO_HardwareReportId",
                table: "Recordings",
                column: "HardwareReportId",
                principalTable: "HardwareReportDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
