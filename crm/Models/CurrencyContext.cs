using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.Models
{
    public class CurrencyContext : DbContext
    {
        public CurrencyContext()
        {

        }
        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().Property(c => c.Updated_at).HasDefaultValue(DateTime.Now);
        }

        public DbSet<Currency> Currency { get; set; }

    }
}
