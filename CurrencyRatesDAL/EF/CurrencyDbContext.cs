using Microsoft.EntityFrameworkCore;
using CurrencyRatesDAL.Models;

namespace CurrencyRatesDAL.EF
{
    public class CurrencyDbContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }

        public CurrencyDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = currencies.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
