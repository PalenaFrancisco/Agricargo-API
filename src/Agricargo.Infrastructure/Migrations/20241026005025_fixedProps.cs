using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agricargo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixedProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFullCapacity",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "Destiny",
                table: "Trips",
                newName: "Destination");

            migrationBuilder.RenameColumn(
                name: "ShipId",
                table: "Ships",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "Reservations",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Destination",
                table: "Trips",
                newName: "Destiny");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Ships",
                newName: "ShipId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reservations",
                newName: "ReservationId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFullCapacity",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
