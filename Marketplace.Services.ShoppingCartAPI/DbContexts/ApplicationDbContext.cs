using Marketplace.Services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.ShoppingCartAPI.DbContexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
    }
}
