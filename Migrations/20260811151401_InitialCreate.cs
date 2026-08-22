using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityDriveManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PointsOfInterest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<double>(type: "double", nullable: false),
                    Longitude = table.Column<double>(type: "double", nullable: false),
                    PoiType = table.Column<string>(type: "varchar(21)", maxLength: 21, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    BuildYear = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointsOfInterest", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Color = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrentSpeed = table.Column<int>(type: "int", nullable: false),
                    VehicleType = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Model = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BatteryLevel = table.Column<double>(type: "double", nullable: true),
                    FuelLevel = table.Column<double>(type: "double", nullable: true),
                    Tonnage = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    StartPointId = table.Column<int>(type: "int", nullable: false),
                    EndPointId = table.Column<int>(type: "int", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Traffic = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_PointsOfInterest_EndPointId",
                        column: x => x.EndPointId,
                        principalTable: "PointsOfInterest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_PointsOfInterest_StartPointId",
                        column: x => x.StartPointId,
                        principalTable: "PointsOfInterest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_EndPointId",
                table: "Trips",
                column: "EndPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_StartPointId",
                table: "Trips",
                column: "StartPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_VehicleId",
                table: "Trips",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "PointsOfInterest");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
