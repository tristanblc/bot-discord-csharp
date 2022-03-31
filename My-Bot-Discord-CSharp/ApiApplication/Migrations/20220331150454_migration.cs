using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiApplication.Migrations
{
    /// <inheritdoc />
    public partial class migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__bus",
                table: "_bus");

            migrationBuilder.RenameTable(
                name: "_bus",
                newName: "Arret");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Arret",
                table: "Arret",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "_shapes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lat = table.Column<float>(type: "real", nullable: false),
                    longit = table.Column<float>(type: "real", nullable: false),
                    sequence = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shapes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_stopTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    stop_id = table.Column<int>(type: "int", nullable: false),
                    stop_sequence = table.Column<float>(type: "real", nullable: false),
                    arrival_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    departure_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__stopTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_trips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    service_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trip_headsign = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    direction_id = table.Column<int>(type: "int", nullable: false),
                    shape_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__trips", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_shapes");

            migrationBuilder.DropTable(
                name: "_stopTimes");

            migrationBuilder.DropTable(
                name: "_trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Arret",
                table: "Arret");

            migrationBuilder.RenameTable(
                name: "Arret",
                newName: "_bus");

            migrationBuilder.AddPrimaryKey(
                name: "PK__bus",
                table: "_bus",
                column: "Id");
        }
    }
}
