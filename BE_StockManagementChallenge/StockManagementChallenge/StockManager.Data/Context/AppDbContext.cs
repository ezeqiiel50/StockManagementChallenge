using Microsoft.EntityFrameworkCore;
using StockManager.Data.StoredProcedures;
using StockManager.Domain.Entities;

namespace StockManager.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }

        public DbSet<Product> Productos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductoSpResult>().HasNoKey();
            modelBuilder.Entity<UserSpResult>().HasNoKey();
        }
    }
}
