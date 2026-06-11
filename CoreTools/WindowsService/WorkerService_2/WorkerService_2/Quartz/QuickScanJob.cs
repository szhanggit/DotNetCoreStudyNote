using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerService.Lib;

namespace WorkerService_2.Quartz
{
    public class QuickScanJob : IJob, IExecuable
    {
        private string name;
        private readonly ILogger<QuickScanJob> _logger;
        private readonly IQuickScanService _quickScanService;
        private readonly IQuick2ScanService _quick2ScanService;
        public QuickScanJob(ILogger<QuickScanJob> logger
            , IQuickScanService quickScanService
            , IQuick2ScanService quick2ScanService
            )
        {
            this._logger = logger;
            this._quickScanService = quickScanService;
            this._quick2ScanService = quick2ScanService;
        }

        public string Name
        {
            get
            {
                return BatchConstant.QuickScanJob;
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
                _logger.LogInformation("This is quick scan job.");
                _quickScanService.Scan("QuickScanJob");
                _quick2ScanService.Scan("QuickScanJob2");
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
