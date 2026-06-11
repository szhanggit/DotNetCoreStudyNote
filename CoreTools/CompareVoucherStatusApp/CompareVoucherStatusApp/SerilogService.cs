using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareVoucherStatusApp
{
    public class SerilogService : ISerilogService
    {
        private ILogger _logger = null;
        public SerilogService(ILogger logger)
        {
            this._logger = logger;
        }

        public void logWarning(string msg)
        {
            _logger.Warning(msg);
        }
    }
}
