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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=DESKTOP-JUI9V07;Database=Competitions;Trusted_Connection=True;");
        //    }
        //}

        public DbSet<Product> Products { get; set; }
    }
}
