using Microsoft.EntityFrameworkCore;
using StockManager.Data.StoredProcedures;

namespace StockManager.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductoSpResult>().HasNoKey();
            modelBuilder.Entity<ProductoCreateSpResult>().HasNoKey();
            modelBuilder.Entity<UserSpResult>().HasNoKey();
        }
    }
}
