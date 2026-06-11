using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CompareVoucherStatusApp
{
    public interface IDataTableToDataListHelper
    {
        IEnumerable<T> ConvertDataTableToList<T>(DataTable table) where T : new();
        DataTable ConvertToDatatable<T>(List<T> data);
    }
}
