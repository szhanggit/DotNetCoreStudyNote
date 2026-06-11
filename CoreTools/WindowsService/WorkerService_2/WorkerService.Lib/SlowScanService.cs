using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerService.Lib
{
    public class SlowScanService : ISlowScanService
    {
        private readonly ILogger<SlowScanService> _logger;
        public SlowScanService(ILogger<SlowScanService> logger)
        {
            this._logger = logger;
        }

        public void Scan(string JobName)
        {
            _logger.Log(LogLevel.Information, string.Format("Slow scan {0}", JobName));
        }
    }
}
