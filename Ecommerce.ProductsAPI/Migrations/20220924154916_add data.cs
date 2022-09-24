using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.ProductsAPI.Migrations
{
    public partial class adddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "ImageUrl", "Name", "Price" },
                values: new object[] { 1, 20.0, "", "Pen", 10.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
