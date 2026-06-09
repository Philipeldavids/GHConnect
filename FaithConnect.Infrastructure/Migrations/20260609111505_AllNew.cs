using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaithConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FamilyName",
                table: "Families",
                newName: "HouseholdName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HouseholdName",
                table: "Families",
                newName: "FamilyName");
        }
    }
}
