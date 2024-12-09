using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace resurec.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HardwareReportDTO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CpuUsage = table.Column<float>(type: "REAL", nullable: true),
                    CpuTemperature = table.Column<float>(type: "REAL", nullable: true),
                    RamUsage = table.Column<float>(type: "REAL", nullable: true),
                    GpuUsage = table.Column<float>(type: "REAL", nullable: true),
                    GpuTemperature = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareReportDTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recordings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    HardwareReportId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recordings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recordings_HardwareReportDTO_HardwareReportId",
                        column: x => x.HardwareReportId,
                        principalTable: "HardwareReportDTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recordings_HardwareReportId",
                table: "Recordings",
                column: "HardwareReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recordings");

            migrationBuilder.DropTable(
                name: "HardwareReportDTO");
        }
    }
}
