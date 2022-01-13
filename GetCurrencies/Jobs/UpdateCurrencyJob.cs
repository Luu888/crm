using crm.Models;
using crm.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetCurrencies.Jobs
{
    class UpdateCurrencyJob : IJob
    {
        public static void UpdateJob()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Job started at: {DateTime.Now}");
            Console.ForegroundColor = ConsoleColor.Gray;
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
        public Task Execute(IJobExecutionContext context)
        {
            UpdateJob();
            return Task.CompletedTask;
        }
    }


}
