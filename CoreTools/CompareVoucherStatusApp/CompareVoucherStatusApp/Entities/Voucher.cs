using System;
using System.Collections.Generic;
using System.Text;

namespace CompareVoucherStatusApp.Entities
{
    public class Voucher
    {
        public string VoucherNumber { get; set; }
        public int ProgramId { get; set; }
        public int ProductId { get; set; }
        public int Status { get; set; }
    }
}
