using CompareVoucherStatusApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompareVoucherStatusApp.Repository
{
    public interface IVoucherRepository
    {
        Task<Voucher> GetByIdAsync(int id);
        List<VoucherMove> GetVoucherStatusFromMove(List<MoveVoucherNumberProgramCode> _inputList);
        int BulkInsertVoucherBuff(List<MoveVoucherNumberProgramCodeClientName> list, string TableName, string DatabaseName);
    }
}
