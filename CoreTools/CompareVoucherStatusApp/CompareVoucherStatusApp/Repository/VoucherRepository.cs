using CompareVoucherStatusApp.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CompareVoucherStatusApp.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly ISetting _configuration;
        private readonly ISqlHelper _sqlHelper;
        private readonly IDataTableToDataListHelper _dataTableToDataListHelper;

        public VoucherRepository(ISetting configuration
            , ISqlHelper sqlHelper
            , IDataTableToDataListHelper dataTableToDataListHelper)
        {
            this._configuration = configuration;
            this._sqlHelper = sqlHelper;
            this._dataTableToDataListHelper = dataTableToDataListHelper;
        }

        public async Task<Voucher> GetByIdAsync(int id)
        {
            var sql = "SELECT VoucherNumber, ProgramId, ProductId, [Status] FROM Voucher with(nolock) WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.MoveConnection))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Voucher>(sql, new { Id = id });
                return result;
            }
        }

        public List<VoucherMove> GetVoucherStatusFromMove(List<MoveVoucherNumberProgramCode> _inputList)
        {
            List<VoucherMove> _outputList = new List<VoucherMove>();
            if (_inputList.Count == 0)
            {
                return _outputList;
            }
            var TypeVoucherNumberProgramCodeParameter = new List<SqlDataRecord>();
            var VoucherNumberProgramCodeMetaData = new SqlMetaData[] {
                    new SqlMetaData("Id", SqlDbType.Int),
                    new SqlMetaData("VoucherNumber", SqlDbType.VarChar,100),
                    new SqlMetaData("ProgramCode", SqlDbType.VarChar,50)
                };

            foreach (var sc in _inputList)
            {
                var record = new SqlDataRecord(VoucherNumberProgramCodeMetaData);
                record.SetValue(0, sc.Id);
                record.SetValue(1, sc.VoucherNumber);
                record.SetValue(2, sc.ProgramCode);
                TypeVoucherNumberProgramCodeParameter.Add(record);
            }

            using (IDbConnection connection = new SqlConnection(_configuration.MoveConnection))
            {
                _outputList = connection.Query<VoucherMove>("spVoucherStatusFromMove", new { @VoucherNumberProgramCodeList = TypeVoucherNumberProgramCodeParameter.AsTableValuedParameter("VoucherNumber_ProgramCode") }, commandType: CommandType.StoredProcedure).AsList();
            }

            return _outputList;
        }

        public int BulkInsertVoucherBuff(List<MoveVoucherNumberProgramCodeClientName> list, string TableName, string DatabaseName)
        {
            var accountTable = StaticMemberHelper.VoucherBufferTable();
            if (list.Count == 0)
            {
                return 0;
            }
            var dataTable = _dataTableToDataListHelper.ConvertToDatatable<MoveVoucherNumberProgramCodeClientName>(list);
            dataTable.TableName = TableName;
            using (var transactionScope = new TransactionScope())
            {
                _sqlHelper.DataTableToSqlServer(dataTable, DatabaseName);
                transactionScope.Complete();
            }

            return 1;
        }
    }
}
