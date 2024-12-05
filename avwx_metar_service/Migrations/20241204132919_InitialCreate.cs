using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace avwx_metar_service.Migrations
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
                name: "Metars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Station = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Time_Dt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    WindDirection_Value = table.Column<int>(type: "int", nullable: false),
                    WindSpeed_Value = table.Column<int>(type: "int", nullable: false),
                    Visibility_Value = table.Column<int>(type: "int", nullable: false),
                    Temperature_Value = table.Column<int>(type: "int", nullable: false),
                    Dewpoint_Value = table.Column<int>(type: "int", nullable: false),
                    Altimeter_Value = table.Column<int>(type: "int", nullable: false),
                    Raw = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metars", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cloud",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Altitude = table.Column<int>(type: "int", nullable: false),
                    MetarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cloud", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cloud_Metars_MetarId",
                        column: x => x.MetarId,
                        principalTable: "Metars",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WxCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MetarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WxCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WxCode_Metars_MetarId",
                        column: x => x.MetarId,
                        principalTable: "Metars",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Cloud_MetarId",
                table: "Cloud",
                column: "MetarId");

            migrationBuilder.CreateIndex(
                name: "IX_WxCode_MetarId",
                table: "WxCode",
                column: "MetarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cloud");

            migrationBuilder.DropTable(
                name: "WxCode");

            migrationBuilder.DropTable(
                name: "Metars");
        }
    }
}
