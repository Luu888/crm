using crm.Models;
using crm.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GetCurrencies
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<CurrencyContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CurrencyConnection"));
            var _context = new CurrencyContext(optionsBuilder.Options);
            var service = new CurrencyService(_context);
            try
            {
                service.Update();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            
        }
    }
}
