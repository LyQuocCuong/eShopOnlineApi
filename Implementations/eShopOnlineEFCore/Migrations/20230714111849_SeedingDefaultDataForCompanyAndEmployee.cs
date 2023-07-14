using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopOnlineEFCore.Migrations
{
    public partial class SeedingDefaultDataForCompanyAndEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Address", "CreatedDate", "IsRemoved", "Name", "Phone", "UpdatedDate" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "LQC Company", "1234567890", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Address", "Code", "CreatedDate", "FirstName", "IsRemoved", "LastName", "Phone", "UpdatedDate", "WorkingStoreId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000101"), "", "ADMIN101", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Henry", false, "Admin", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"));
        }
    }
}
