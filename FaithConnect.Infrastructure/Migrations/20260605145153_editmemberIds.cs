using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaithConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editmemberIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Churches_Id",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Families_Id",
                table: "Members");

            migrationBuilder.CreateIndex(
                name: "IX_Members_ChurchId",
                table: "Members",
                column: "ChurchId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_FamilyId",
                table: "Members",
                column: "FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Churches_ChurchId",
                table: "Members",
                column: "ChurchId",
                principalTable: "Churches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Families_FamilyId",
                table: "Members",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Churches_ChurchId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Families_FamilyId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_ChurchId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_FamilyId",
                table: "Members");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Churches_Id",
                table: "Members",
                column: "Id",
                principalTable: "Churches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Families_Id",
                table: "Members",
                column: "Id",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
