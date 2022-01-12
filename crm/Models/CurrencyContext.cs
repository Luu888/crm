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
        public DbSet<Currency> Currency { get; set; }
    }
}
