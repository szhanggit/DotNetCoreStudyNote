using CompareVoucherStatusApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareVoucherStatusApp.Repository
{
    public interface IExtendVoucherRepository
    {
        List<MoveVoucherNumberProgramCode> GetExtendVoucherSyncInfo(int Step);
        List<MoveVoucherNumberProgramCodeClientName> GetExtendVoucherSyncInfo2(int Step);
        void UpdateVoucherStatus(List<VoucherMove> _list);
    }
}
