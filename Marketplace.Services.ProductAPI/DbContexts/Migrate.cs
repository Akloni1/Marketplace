using AutoMapper;
using Marketplace.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Marketplace.Services.ProductAPI.DbContexts
{
    public class Migrate
    {
        private readonly ApplicationDbContext _db;

        public Migrate(ApplicationDbContext db)
        {
            _db = db;
        }
        public void MigrateProduct()
        {
            _db.Database.Migrate();
        }

        public void InitializeProducts()
        {
            List<Product> products = new List<Product>
            {
               new Product{
                Id = 0,
                Name = "Мужские часы",
                Price = 15000,
                Description = "Мужские часы кварцевые.",
                ImageUrl = "/Images/chasi.jpeg",
                CategoryName = "Техника"
               },
               new Product{
                Id = 0,
                Name = "Кофемашина",
                Price = 30000,
                Description = "Кофемашина.",
                ImageUrl = "/Images/coffecar.jpg",
                CategoryName = "Техника"
               },
             new Product{
                Id = 0,
                Name = "Iphone 11",
                Price = 45000,
                Description = "Iphone 11.",
                ImageUrl = "/Images/iphone11.jpg",
                CategoryName = "Техника"
               },
              new Product{
                Id = 0,
                Name = "Кофе",
                Price = 450,
                Description = "Кофе.",
                ImageUrl = "/Images/koffe.jpeg",
                CategoryName = "Продукты питания"
               },
              new Product{
                Id = 0,
                Name = "Колонка JBL",
                Price = 16990,
                Description = "Колонка JBL.",
                ImageUrl = "/Images/kolonka.jpg",
                CategoryName = "Техника"
               },
             new Product{
                Id = 0,
                Name = "Мультиварка",
                Price = 7490,
                Description = "Мультиварка.",
                ImageUrl = "/Images/multivarka.jpg",
                CategoryName = "Техника"
               },
              new Product{
                Id = 0,
                Name = "Обогреватель",
                Price = 9990,
                Description = "Обогреватель.",
                ImageUrl = "/Images/obogrevatel.jpg",
                CategoryName = "Техника"
               },
               new Product{
                Id = 0,
                Name = "Утюг",
                Price = 16000,
                Description = "Утюг.",
                ImageUrl = "/Images/ytug.jpg",
                CategoryName = "Техника"
               },
               new Product{
                Id = 0,
                Name = "Зонт",
                Price = 1500,
                Description = "Зонт.",
                ImageUrl = "/Images/zont.jpg",
                CategoryName = "Аксессуары для повседневного использования"
               },
            };

            if (!_db.Products.Any())
            {
                _db.Products.AddRange(products);
                _db.SaveChanges();
            }
        }
    }
}
