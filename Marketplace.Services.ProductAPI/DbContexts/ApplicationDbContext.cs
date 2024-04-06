using Marketplace.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Мужские часы",
                Price = 15000,
                Description = "Мужские часы кварцевые.",
                ImageUrl = "/Images/logo.png",
                CategoryName = "Техника"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Name = "IPhone 11",
                Price = 60000,
                Description = "IPhone 11.",
                ImageUrl = "/Images/logo.png",
                CategoryName = "Техника"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Name = "Футболка",
                Price = 4000,
                Description = "Футболка размер M",
                ImageUrl = "/Images/logo.png",
                CategoryName = "Одежда"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 4,
                Name = "Шуруп-глухарь",
                Price = 150,
                Description = "Шуруп-глухарь 12х180 мм для крепления деревянных лаг и реек. Цена указана за 8 шт.",
                ImageUrl = "/Images/logo.png",
                CategoryName = "Ремонт"
            });
        }

        public DbSet<Product> Products { get; set; }
    }
}
