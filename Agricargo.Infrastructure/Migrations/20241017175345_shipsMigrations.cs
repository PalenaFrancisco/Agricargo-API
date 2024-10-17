using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agricargo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class shipsMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Users_Id",
                table: "Ships");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Ships",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Ships_Id",
                table: "Ships",
                newName: "IX_Ships_CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Users_CompanyId",
                table: "Ships",
                column: "CompanyId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Users_CompanyId",
                table: "Ships");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Ships",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Ships_CompanyId",
                table: "Ships",
                newName: "IX_Ships_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Users_Id",
                table: "Ships",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
