using GetCurrencies.Modals;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetCurrencies
{
    public class Scheduler : IHostedService
    {
        public IScheduler SchedulerS { get; set; }
        private readonly IJobFactory jobFactory;
        private readonly JobModal jobModal;
        private readonly ISchedulerFactory schedulerFactory;
        public Scheduler(ISchedulerFactory schedulerFactory, JobModal jobModal, IJobFactory jobFactory)
        {
            this.schedulerFactory = schedulerFactory;
            this.jobModal = jobModal;
            this.jobFactory = jobFactory;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            SchedulerS = await schedulerFactory.GetScheduler();
            SchedulerS.JobFactory = jobFactory;
            IJobDetail jobDetail = CreateJob(jobModal);
            ITrigger trigger = CreateTrigger(jobModal);
            await SchedulerS.ScheduleJob(jobDetail, trigger, cancellationToken);
            await SchedulerS.Start(cancellationToken);
             
        }

        private ITrigger CreateTrigger(JobModal jobModal)
        {
            return TriggerBuilder.Create()
                .WithIdentity(jobModal.JobId.ToString())
                .WithCronSchedule(jobModal.CronExpression)
                .WithDescription(jobModal.JobName)
                .Build();
        }

        private IJobDetail CreateJob(JobModal jobModal)
        {
            return JobBuilder.Create(jobModal.JobType)
                .WithIdentity(jobModal.JobId.ToString())
                .WithDescription(jobModal.JobName.ToString())
                .Build();

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            
        }
    }
}
