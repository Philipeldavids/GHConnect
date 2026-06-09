using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaithConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Churchmodelupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultAllowedRadiusMeters",
                table: "Churches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Churches",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Churches",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Churches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultAllowedRadiusMeters",
                table: "Churches");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Churches");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Churches");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Churches");
        }
    }
}
