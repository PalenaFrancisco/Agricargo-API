using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agricargo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class shipsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Ships",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ships_Id",
                table: "Ships",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Users_Id",
                table: "Ships",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Users_Id",
                table: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Ships_Id",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Ships");
        }
    }
}
