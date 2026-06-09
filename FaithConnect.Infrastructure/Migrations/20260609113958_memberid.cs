using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaithConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class memberid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Churches_ChurchId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Churches_ChurchId",
                table: "AspNetUsers",
                column: "ChurchId",
                principalTable: "Churches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Churches_ChurchId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Churches_ChurchId",
                table: "AspNetUsers",
                column: "ChurchId",
                principalTable: "Churches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
