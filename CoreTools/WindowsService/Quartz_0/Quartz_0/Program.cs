using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Quartz_0.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quartz_0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Add Quartz services
                    services.AddHostedService<QuartzHostedService>();
                    services.AddSingleton<IJobFactory, SingletonJobFactory>();
                    services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                    // Add our job
                    services.AddSingleton<RemindersJob>();
                    services.AddSingleton(new JobSchedule(
                        jobType: typeof(RemindersJob),
                        cronExpression: "0/5 * * * * ?")); // run every 5 sec
                }).UseWindowsService();
    }
}
