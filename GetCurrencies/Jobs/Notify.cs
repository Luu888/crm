using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetCurrencies.Jobs
{
    class Notify : IJob
    {
        private readonly ILogger<Notify> _logger;
        public Notify(ILogger<Notify> logger)
        {
            this._logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Started at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
