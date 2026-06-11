using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerService.Lib
{
    public interface ISlowScanService
    {
        void Scan(string JobName);
    }
}
