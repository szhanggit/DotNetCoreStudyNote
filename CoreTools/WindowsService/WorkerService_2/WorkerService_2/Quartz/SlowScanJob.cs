using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerService.Lib;

namespace WorkerService_2.Quartz
{
    public class SlowScanJob : IJob, IExecuable
    {
        private string name;
        private readonly ILogger<SlowScanJob> _logger;
        private readonly ISlowScanService _slowScanService;
        private readonly ISlow2ScanService _slow2ScanService;
        public SlowScanJob(ILogger<SlowScanJob> logger
            , ISlowScanService slowScanService
            , ISlow2ScanService slow2ScanService
            )
        {
            this._logger = logger;
            this._slowScanService = slowScanService;
            this._slow2ScanService = slow2ScanService;
        }

        public string Name
        {
            get
            {
                return BatchConstant.SlowScanJob;
            }
            set
            {
                name = value;
            }
        }

        public Task Execute(IJobExecutionContext context)
        {
            SingleExecutingPool.Instance.Enqueue(this);
            return Task.CompletedTask;
        }

        public bool Execute()
        {
            bool isSuccess = false;
            try
            {
                _logger.LogDebug(string.Format("Start processing - {0}", Name));
                _logger.LogInformation("This is slow scan job.");
                _slowScanService.Scan("SlowScanJob");
                _slow2ScanService.Scan("SlowScanJob2");
                _logger.LogDebug(string.Format("Finish processing - {0}", Name));
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Fail to process - {0}", Name));
                _logger.LogError(new EventId(), ex, "");
            }

            return isSuccess;
        }
    }
}
