using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiApplication.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_bus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    stop_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    xlocation = table.Column<float>(type: "real", nullable: false),
                    ylocation = table.Column<float>(type: "real", nullable: false),
                    ville = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__bus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_lignes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    route_short_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    route_desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    route_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    route_text_color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__lignes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_bus");

            migrationBuilder.DropTable(
                name: "_lignes");
        }
    }
}
