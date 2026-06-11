using System;
using System.Collections.Generic;
using System.Text;

namespace CompareVoucherStatusApp
{
    public interface ISerilogService
    {
        void logWarning(string msg);
    }
}
