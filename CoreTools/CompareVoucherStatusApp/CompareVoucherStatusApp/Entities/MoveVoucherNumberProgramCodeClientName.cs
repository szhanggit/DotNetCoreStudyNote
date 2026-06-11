using System;
using System.Collections.Generic;
using System.Text;

namespace CompareVoucherStatusApp.Entities
{
    public class MoveVoucherNumberProgramCodeClientName : MoveVoucherNumberProgramCode
    {
        public string EndClient { get; set; }
    }
}
