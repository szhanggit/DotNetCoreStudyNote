using CompareVoucherStatusApp;
using CompareVoucherStatusApp.Entities;
using CompareVoucherStatusApp.Repository;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestCompareVoucherStatusApp
{    
    public class TestExtendVoucherRepository
    {
        [Fact]
        public void TestGetExtendVoucherSyncInfo()
        {
            ISetting configuration = new MockSettingService();
            ExtendVoucherRepository vr = new ExtendVoucherRepository(configuration);
            List<MoveVoucherNumberProgramCode> _inputList = new List<MoveVoucherNumberProgramCode>();
            _inputList = vr.GetExtendVoucherSyncInfo(1);
        }

        [Fact]
        public void TestUpdateVoucherStatus()
        {
            ISetting configuration = new MockSettingService();
            ExtendVoucherRepository vr = new ExtendVoucherRepository(configuration);
            List<VoucherMove> _list = new List<VoucherMove> { new VoucherMove { Id = 252, Status = 4} };
            vr.UpdateVoucherStatus(_list);
        }
    }
}
