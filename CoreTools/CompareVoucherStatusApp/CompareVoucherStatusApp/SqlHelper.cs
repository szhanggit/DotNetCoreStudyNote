using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CompareVoucherStatusApp
{
    public class SqlHelper : ISqlHelper
    {
        private readonly ISetting _configuration;
        public SqlHelper(ISetting configuration)
        {
            this._configuration = configuration;
        }

        public void DataTableToSqlServer(DataTable dataTable, string DatabaseName)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(_configuration.MoveConnection, SqlBulkCopyOptions.UseInternalTransaction))
            {
                bulkCopy.DestinationTableName = dataTable.TableName;
                foreach (DataColumn column in dataTable.Columns)
                {
                    bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }

                bulkCopy.WriteToServer(dataTable);
            }
        }
    }
}
