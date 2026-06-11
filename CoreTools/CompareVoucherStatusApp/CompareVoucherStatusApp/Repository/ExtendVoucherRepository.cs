using CompareVoucherStatusApp.Entities;
using Dapper;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CompareVoucherStatusApp.Repository
{
    public class ExtendVoucherRepository : IExtendVoucherRepository
    {
        private readonly ISetting configuration;
        private List<MoveVoucherNumberProgramCode> _list = null;
        private List<MoveVoucherNumberProgramCodeClientName> _list2 = null;
        public ExtendVoucherRepository(ISetting configuration)
        {
            this.configuration = configuration;
            this._list = new List<MoveVoucherNumberProgramCode>();
            this._list2 = new List<MoveVoucherNumberProgramCodeClientName>();
        }

        public List<MoveVoucherNumberProgramCode> GetExtendVoucherSyncInfo(int Step)
        {
            using (var connection = new SqlConnection(configuration.LocalConnection))
            {
                connection.Open();
                _list = connection.Query<MoveVoucherNumberProgramCode>(ScriptManager.GetExtendVoucherSyncInfo(), new { @Step = Step }).AsList();
                return _list;
            }
        }

        public List<MoveVoucherNumberProgramCodeClientName> GetExtendVoucherSyncInfo2(int Step)
        {
            using (var connection = new SqlConnection(configuration.LocalConnection))
            {
                connection.Open();
                _list2 = connection.Query<MoveVoucherNumberProgramCodeClientName>(ScriptManager.GetExtendVoucherSyncInfo2(), new { @Step = Step }).AsList();
                return _list2;
            }
        }

        public void UpdateVoucherStatus(List<VoucherMove> _list)
        {
            if (_list.Count == 0)
            {
                return;
            }

            var TypeBeneficiaryInfoIdParameter = new List<SqlDataRecord>();
            var VoucherStatusMetaData = new SqlMetaData[] {
                    new SqlMetaData("Id", SqlDbType.Int),
                    new SqlMetaData("Status", SqlDbType.Int)
                };

            foreach (var bi in _list)
            {
                var record = new SqlDataRecord(VoucherStatusMetaData);
                record.SetValue(0, bi.Id);
                record.SetValue(1, bi.Status);
                TypeBeneficiaryInfoIdParameter.Add(record);
            }

            using (var connection = new SqlConnection(configuration.LocalConnection))
            {
                connection.Execute("spUpdateVoucherStatus", new { @VoucherStatusList = TypeBeneficiaryInfoIdParameter.AsTableValuedParameter("Voucher_Status") }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
