using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerService.Lib
{
    public class Quick2ScanService : IQuick2ScanService
    {
        private readonly ILogger<Quick2ScanService> _logger;
        public Quick2ScanService(ILogger<Quick2ScanService> logger)
        {
            this._logger = logger;
        }

        public void Scan(string JobName)
        {
            _logger.Log(LogLevel.Information, string.Format("Quick2 scan {0}", JobName));
        }
    }
}
