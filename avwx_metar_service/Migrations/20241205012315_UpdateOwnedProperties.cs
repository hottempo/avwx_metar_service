using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace avwx_metar_service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOwnedProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Visibility_Value",
                table: "Metars",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Visibility_Value",
                table: "Metars",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);
        }
    }
}
