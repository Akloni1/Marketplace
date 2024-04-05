using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Services.ProductAPI.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Техника", "Мужские часы кварцевые.", "C:\\Users\\Михаил\\Desktop\\Marketplace\\Image\\screw.jpeg", "Мужские часы", 15000.0 },
                    { 2, "Техника", "IPhone 11.", "C:\\Users\\Михаил\\Desktop\\Marketplace\\Image\\screw.jpeg", "IPhone 11", 60000.0 },
                    { 3, "Одежда", "Футболка размер M", "C:\\Users\\Михаил\\Desktop\\Marketplace\\Image\\screw.jpeg", "Футболка", 4000.0 },
                    { 4, "Ремонт", "Шуруп-глухарь 12х180 мм для крепления деревянных лаг и реек. Цена указана за 8 шт.", "C:\\Users\\Михаил\\Desktop\\Marketplace\\Image\\screw.jpeg", "Шуруп-глухарь", 150.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
