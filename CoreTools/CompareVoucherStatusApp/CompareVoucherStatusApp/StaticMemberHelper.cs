using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CompareVoucherStatusApp
{
    public static class StaticMemberHelper
    {
        private static readonly DataTable _voucherBufferTable;

        static StaticMemberHelper()
        {
            #region VoucherBufferTable Table
            _voucherBufferTable = new DataTable("TempVoucher20220308");
            _voucherBufferTable.Columns.Add("Id", typeof(int));
            _voucherBufferTable.Columns.Add("VoucherNumber", typeof(string));
            _voucherBufferTable.Columns.Add("ProgramCode", typeof(string));
            _voucherBufferTable.Columns.Add("EndClient", typeof(string));
            #endregion
        }

        public static DataTable VoucherBufferTable()
        {
            return _voucherBufferTable.Clone();
        }
    }
}
