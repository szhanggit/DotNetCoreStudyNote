using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerService.Lib
{
    public class Slow2ScanService : ISlow2ScanService
    {
        private readonly ILogger<Slow2ScanService> _logger;
        public Slow2ScanService(ILogger<Slow2ScanService> logger)
        {
            this._logger = logger;
        }

        public void Scan(string JobName)
        {
            _logger.Log(LogLevel.Information, string.Format("Slow2 scan {0}", JobName));
        }
    }
}
