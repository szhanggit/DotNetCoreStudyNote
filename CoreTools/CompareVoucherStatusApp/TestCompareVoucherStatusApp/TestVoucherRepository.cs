using CompareVoucherStatusApp;
using CompareVoucherStatusApp.Entities;
using CompareVoucherStatusApp.Repository;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestCompareVoucherStatusApp
{
    public class TestVoucherRepository
    {
        [Fact]
        public void TestGetVoucherStatusFromMove()
        {
            ISetting configuration = new MockSettingService();
            VoucherRepository vr = new VoucherRepository(configuration);
            List<MoveVoucherNumberProgramCode> _inputList = new List<MoveVoucherNumberProgramCode> { 
                new MoveVoucherNumberProgramCode{ Id = 1, VoucherNumber = "8601002610027007651448952449", ProgramCode = "00001" },
                new MoveVoucherNumberProgramCode{ Id = 2, VoucherNumber = "8601002610027138268954407113", ProgramCode = "00001" },
                new MoveVoucherNumberProgramCode{ Id = 3, VoucherNumber = "8601002610027242044765081485", ProgramCode = "00001" },
            };
            vr.GetVoucherStatusFromMove(_inputList);
        }
    }
}
