using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerService.Lib
{
    public class Cal2Service : ICal2Service
    {
        private readonly ILogger<Cal2Service> _logger;
        public Cal2Service(ILogger<Cal2Service> logger)
        {
            this._logger = logger;
        }

        public void Scan(int i)
        {
            _logger.Log(LogLevel.Information, string.Format("Add2 {0}", i));
        }
    }
}
