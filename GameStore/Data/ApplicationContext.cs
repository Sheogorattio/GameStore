using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    public class ApplicationContext : DbContext 
    {
        public ApplicationContext(DbContextOptions options) : base(options) 
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> Lines { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasIndex(e => e.Name);
            modelBuilder.Entity<Product>().HasIndex(e => e.PurchasePrice);
            modelBuilder.Entity<Product>().HasIndex(e => e.RetailPrice);
            modelBuilder.Entity<Category>().HasIndex(e => e.Name);
            modelBuilder.Entity<Category>().HasIndex(e => e.Description);
        }
    }
}
