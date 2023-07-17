using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopOnlineEFCore.Migrations
{
    public partial class Rename_IsRemoved_To_IsDeleted_AllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRemoved",
                table: "Store",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsRemoved",
                table: "Product",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsRemoved",
                table: "Employee",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsRemoved",
                table: "Customer",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsRemoved",
                table: "Company",
                newName: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Store",
                newName: "IsRemoved");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Product",
                newName: "IsRemoved");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Employee",
                newName: "IsRemoved");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Customer",
                newName: "IsRemoved");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Company",
                newName: "IsRemoved");
        }
    }
}
