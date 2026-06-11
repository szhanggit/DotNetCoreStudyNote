using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CompareVoucherStatusApp
{
    public interface ISqlHelper
    {
        void DataTableToSqlServer(DataTable dataTable, string DatabaseName);
    }
}
