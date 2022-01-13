using GetCurrencies.JobFactory;
using GetCurrencies.Jobs;
using GetCurrencies.Modals;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;


namespace GetCurrencies
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IJobFactory, MyJobFactory>();
                    services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                    services.AddSingleton<UpdateCurrencyJob>();
                    services.AddSingleton(new JobModal(Guid.NewGuid(), typeof(UpdateCurrencyJob), "Update Currency Job", "0/10 * * * * ?"));
                    services.AddHostedService<Scheduler>();
                }
            );
    }
}
