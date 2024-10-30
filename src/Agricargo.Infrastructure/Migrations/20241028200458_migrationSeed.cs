using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Agricargo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migrationSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyName", "Email", "Name", "Password", "Phone", "TypeUser" },
                values: new object[,]
                {
                    { new Guid("1f11bb16-72d5-4032-8a3b-6f125d4ee653"), "El Maruco CIA", "mario@gmail.com", "Mario Massonnat", "$2a$11$UYYYIX4OOYOPxYkF/QI9vuJIhiYRdDOdrIkiHh0DVH6FKv9JKOigi", "153252", "Admin" },
                    { new Guid("b94d4613-8450-4fef-bdd2-6edc89a30cf3"), "Pale SRL", "pale@gmail.com", "Francisco Palena", "$2a$11$2r2kMNLDl9o2aVg8nuO/Y.etd4ThWIDKHIc8nIxwPd1vQpIa4y6o6", "1986", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Phone", "TypeUser" },
                values: new object[,]
                {
                    { new Guid("d609a32a-91ba-495a-900e-9138e6619ad3"), "pablo@gmail.com", "Pablo", "$2a$11$ApXY.5ywnPtJ0I1pGb19WOpXuGMQ03MS3HFI2.GWf4rec58VO./se", "19864343", "Client" },
                    { new Guid("d63af9c3-34de-44ab-9ea7-98866f260bbb"), "emi@gmail.com", "Emiliano", "$2a$11$yNyKVoOe/OX71vxfO434Ruhm6L4D0FvbuW/i8LYcKOQ51XveFwez2", "1923486", "Client" },
                    { new Guid("ec61c741-5f6d-487e-959e-450e96b465c5"), "sys_admin@gmail.com", "web master", "$2a$11$p.Jx14COotRbeLuRS3BtUOeWmPJcTYQRjc9vPZVfVddtuvXtCUesm", "1242214", "SuperAdmin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f11bb16-72d5-4032-8a3b-6f125d4ee653"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b94d4613-8450-4fef-bdd2-6edc89a30cf3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d609a32a-91ba-495a-900e-9138e6619ad3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d63af9c3-34de-44ab-9ea7-98866f260bbb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ec61c741-5f6d-487e-959e-450e96b465c5"));

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
    }
}
