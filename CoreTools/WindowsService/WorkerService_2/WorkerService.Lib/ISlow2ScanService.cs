using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerService.Lib
{
    public interface ISlow2ScanService
    {
        void Scan(string JobName);
    }
}
