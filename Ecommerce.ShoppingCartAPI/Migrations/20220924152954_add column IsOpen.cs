using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.ShoppingCartAPI.Migrations
{
    public partial class addcolumnIsOpen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Open",
                table: "ShoppingCarts",
                newName: "IsOpened");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsOpened",
                table: "ShoppingCarts",
                newName: "Open");
        }
    }
}
