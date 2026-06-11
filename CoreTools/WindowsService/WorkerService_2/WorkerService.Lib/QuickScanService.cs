using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerService.Lib
{
    public class QuickScanService : IQuickScanService
    {
        private readonly ILogger<QuickScanService> _logger;

        public QuickScanService(ILogger<QuickScanService> logger)
        {
            this._logger = logger;
        }

        public void Scan(string JobName)
        {
            _logger.Log(LogLevel.Information, string.Format("Quick scan {0}", JobName));
        }
    }
}
