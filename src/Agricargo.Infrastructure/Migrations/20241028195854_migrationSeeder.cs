using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Agricargo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migrationSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyName", "Email", "Name", "Password", "Phone", "TypeUser" },
                values: new object[,]
                {
                    { new Guid("0699fa36-bc12-4937-96af-899046fab1d0"), "Pale SRL", "pale@gmail.com", "Francisco Palena", "pale1234", "1986", "Admin" },
                    { new Guid("379b0d9e-9042-4136-a4a9-12f430cea28f"), "El Maruco CIA", "mario@gmail.com", "Mario Massonnat", "mario1234", "153252", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Phone", "TypeUser" },
                values: new object[,]
                {
                    { new Guid("48e4c99e-5859-4770-9d6b-8bcd8840f856"), "emi@gmail.com", "Emiliano", "emi1234", "1923486", "Client" },
                    { new Guid("7983ab63-d0ba-412d-97b2-c5170924f4fa"), "pablo@gmail.com", "Pablo", "pablo1234", "19864343", "Client" },
                    { new Guid("c09fc353-580d-4515-b3c3-10e73f64dc1b"), "sys_admin@gmail.com", "web master", "superadmin1234", "1242214", "SuperAdmin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0699fa36-bc12-4937-96af-899046fab1d0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("379b0d9e-9042-4136-a4a9-12f430cea28f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("48e4c99e-5859-4770-9d6b-8bcd8840f856"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7983ab63-d0ba-412d-97b2-c5170924f4fa"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c09fc353-580d-4515-b3c3-10e73f64dc1b"));
        }
    }
}
