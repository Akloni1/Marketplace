using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Services.ShoppingCartAPI.Migrations
{
    public partial class fixBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CartDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CartDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
