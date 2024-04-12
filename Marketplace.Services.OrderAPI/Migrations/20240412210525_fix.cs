using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Services.OrderAPI.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderDetails",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderDetails",
                newName: "ProductId");
        }
    }
}
