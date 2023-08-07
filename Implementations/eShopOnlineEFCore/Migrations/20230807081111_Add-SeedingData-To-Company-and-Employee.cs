using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopOnlineEFCore.Migrations
{
    public partial class AddSeedingDataToCompanyandEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Address", "CreatedDateUtcZero", "IsDeleted", "Name", "Phone", "UpdatedDateUtcZero" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "", new DateTime(2023, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), false, "LQC Company", "1234567890", new DateTime(2023, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Address", "Code", "CreatedDateUtcZero", "FirstName", "IsDeleted", "LastName", "Phone", "UpdatedDateUtcZero", "WorkingStoreId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000101"), null, "ADMIN101", new DateTime(2023, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Henry", false, "Admin", "0949995598", new DateTime(2023, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), null });
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
