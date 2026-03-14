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
            modelBuilder.Entity<ProductSpResult>().HasNoKey();
            modelBuilder.Entity<ProductIdSpResult>().HasNoKey();
            modelBuilder.Entity<UserSpResult>().HasNoKey();
        }
    }
}
