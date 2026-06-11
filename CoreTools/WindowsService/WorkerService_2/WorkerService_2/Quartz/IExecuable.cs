using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerService_2.Quartz
{
    public interface IExecuable
    {
        string Name { get; set; }
        bool Execute();
    }
}
