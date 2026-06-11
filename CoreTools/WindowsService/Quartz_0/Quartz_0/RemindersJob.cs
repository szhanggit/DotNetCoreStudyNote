using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Quartz_0
{
    public class RemindersJob : IJob
    {
        private readonly ILogger<RemindersJob> _logger;
        public RemindersJob(ILogger<RemindersJob> logger)
        {
            this._logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            //Logs($"{DateTime.Now} [Reminders Service called]" + Environment.NewLine);
            _logger.LogInformation($"{DateTime.Now} [Reminders Service called]" + Environment.NewLine);

            return Task.CompletedTask;
        }
        public void Logs(string message)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Quartz");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, "Logs.txt");
            using FileStream fstream = new FileStream(path, FileMode.Create);
            using TextWriter writer = new StreamWriter(fstream);
            writer.WriteLine(message);
        }
    }
}
