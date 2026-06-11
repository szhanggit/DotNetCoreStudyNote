using Microsoft.Extensions.Logging;
using System;

namespace WorkerService.Lib
{
    public class CalService : ICalService
    {
        private readonly ILogger<CalService> _logger;
        public CalService(ILogger<CalService> logger)
        {
            this._logger = logger;
        }

        public void Add(int i)
        {
            _logger.Log(LogLevel.Information, string.Format("{0} needs to be added.", i));
        }
    }
}
