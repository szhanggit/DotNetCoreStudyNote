using System;
using System.Collections.Generic;
using System.Text;

namespace CompareVoucherStatusApp
{
    public interface ISetting
    {
        public string MoveConnection { get; }
        public string LocalConnection { get; }
        public int RunningStep { get; }
    }
}
