using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiApplication.Migrations
{
    /// <inheritdoc />
    public partial class j : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_lignes");

            migrationBuilder.DropTable(
                name: "_shapes");

            migrationBuilder.DropTable(
                name: "_stopTimes");

            migrationBuilder.DropTable(
                name: "_trips");

            migrationBuilder.DropTable(
                name: "Arret");

            migrationBuilder.AddColumn<decimal>(
                name: "UserDiscordId",
                table: "_rappels",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserDiscordId",
                table: "_rappels");

            migrationBuilder.CreateTable(
                name: "_lignes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    route_desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    route_short_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    route_text_color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    route_type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__lignes", x => x.Id);
                });

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
                    arrival_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    departure_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    stop_id = table.Column<int>(type: "int", nullable: false),
                    stop_sequence = table.Column<float>(type: "real", nullable: false)
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
                    direction_id = table.Column<int>(type: "int", nullable: false),
                    service_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    shape_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trip_headsign = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__trips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Arret",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    stop_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ville = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    xlocation = table.Column<float>(type: "real", nullable: false),
                    ylocation = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arret", x => x.Id);
                });
        }
    }
}
