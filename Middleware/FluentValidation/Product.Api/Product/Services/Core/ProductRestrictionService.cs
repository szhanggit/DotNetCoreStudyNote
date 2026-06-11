using Dapper;
using Domain.Models;
using Microsoft.SqlServer.Server;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.Data;
using TXC.Common.MessageContract;
using TXC.Common.MessageContract.Product;
using TXC.Proto.Product;

namespace Services.Core
{
    public class ProductRestrictionService : IProductRestrictionService
    {
        private readonly IDapperOperation _dapperOperation;
        private readonly ITX2ServiceBusSender _txcServiceBusSender;

        public ProductRestrictionService()
        {

        }

        public ProductRestrictionService(
            IDapperOperation dapperOperation
            , ITX2ServiceBusSender txcServiceBusSender)
        {
            _dapperOperation = dapperOperation;
            _txcServiceBusSender = txcServiceBusSender;
        }

        public async Task<int> InsertTimeRestriction(List<ProductRedeemTimeRestrictionSet> _inputList, int ProductId, string UserName, IDbConnection _dbConnection)
        {
            try
            {
                CommandDefinition commandDefinition;
                DynamicParameters parameters = new DynamicParameters();

                var TypeRedeemTimeRestrictionParameter = new List<SqlDataRecord>();
                var TimeRestrictionMetaData = new SqlMetaData[] {
                    new SqlMetaData("DayOfTheWeek", SqlDbType.TinyInt),
                    new SqlMetaData("StartTime", SqlDbType.Time),
                    new SqlMetaData("EndTime", SqlDbType.Time)
                };

                foreach (var sc in _inputList)
                {
                    var record = new SqlDataRecord(TimeRestrictionMetaData);
                    record.SetValue(0, sc.DayOfTheWeek);
                    record.SetValue(1, sc.StartTime);
                    record.SetValue(2, sc.EndTime);
                    TypeRedeemTimeRestrictionParameter.Add(record);
                }

                parameters.Add("@product_id", ProductId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@UserName", UserName, DbType.String, ParameterDirection.Input);
                parameters.Add("@TimeRestrictionList", TypeRedeemTimeRestrictionParameter.AsTableValuedParameter("product.TimeRestrictionListType"));
                parameters.Add("@Result", default(int), DbType.Int32, ParameterDirection.Output);

                commandDefinition = new CommandDefinition("product.sp_ins_time_restriction", commandType: CommandType.StoredProcedure,
                                                        parameters: parameters, cancellationToken: default);
                var dbaffectedRows = await _dapperOperation.ProcessSql<ExecuteCommand, int>(_dbConnection, commandDefinition);
                int _result = parameters.Get<int>("@Result");
                return _result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<IEnumerable<ProductRedeemTimeRestrictionSet>> GetTimeRestrictionList(int ProductId, IDbConnection _dbConnection)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@product_id", ProductId, DbType.Int32, ParameterDirection.Input);

            CommandDefinition commandDefinition = new CommandDefinition("product.sp_sel_time_restriction_list", commandType: CommandType.StoredProcedure,
                                                                        parameters: parameters, cancellationToken: default);

            var dbResult = await _dapperOperation.ProcessSql<SelectMany<ProductRedeemTimeRestrictionSet>, IEnumerable<ProductRedeemTimeRestrictionSet>>(_dbConnection, commandDefinition);

            return dbResult;
        }

        public async Task<int> DeleteTimeRestriction(int ProductId, string UserName, IDbConnection _dbConnection)
        {
            CommandDefinition commandDefinition;
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@product_id", ProductId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@UserName", UserName, DbType.String, ParameterDirection.Input);
            parameters.Add("@Result", default(int), DbType.Int32, ParameterDirection.Output);

            commandDefinition = new CommandDefinition("product.sp_del_time_restriction", commandType: CommandType.StoredProcedure,
                                                    parameters: parameters, cancellationToken: default);
            var dbaffectedRows = await _dapperOperation.ProcessSql<ExecuteCommand, int>(_dbConnection, commandDefinition);
            int _result = parameters.Get<int>("@Result");
            return _result;
        }

        public async Task<int> InsertDateRestriction(List<DateTime> _inputList, int ProductId, string UserName, IDbConnection _dbConnection)
        {
            try
            {
                CommandDefinition commandDefinition;
                DynamicParameters parameters = new DynamicParameters();

                var TypeDateRestrictionParameter = new List<SqlDataRecord>();
                var DateRestrictionCodeMetaData = new SqlMetaData[] {
                    new SqlMetaData("Date", SqlDbType.DateTime)
                };

                foreach (var sc in _inputList)
                {
                    var record = new SqlDataRecord(DateRestrictionCodeMetaData);
                    record.SetValue(0, sc);
                    TypeDateRestrictionParameter.Add(record);
                }

                parameters.Add("@product_id", ProductId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@UserName", UserName, DbType.String, ParameterDirection.Input);
                parameters.Add("@DateRestrictionList", TypeDateRestrictionParameter.AsTableValuedParameter("general.DateTimeListType"));
                parameters.Add("@Result", default(int), DbType.Int32, ParameterDirection.Output);

                commandDefinition = new CommandDefinition("product.sp_ins_date_restriction", commandType: CommandType.StoredProcedure,
                                                        parameters: parameters, cancellationToken: default);
                var dbaffectedRows = await _dapperOperation.ProcessSql<ExecuteCommand, int>(_dbConnection, commandDefinition);
                int _result = parameters.Get<int>("@Result");
                return _result;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<IEnumerable<ProductRedeemDateRestrictionSet>> GetDateRestrictionList(int ProductId, IDbConnection _dbConnection)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@product_id", ProductId, DbType.Int32, ParameterDirection.Input);
            IEnumerable<ProductRedeemDateRestrictionSet> dbResult = null;

            CommandDefinition commandDefinition = new CommandDefinition("product.sp_sel_redeem_date_blacklist", commandType: CommandType.StoredProcedure,
                                                                        parameters: parameters, cancellationToken: default);

            dbResult = await _dapperOperation.ProcessSql<SelectMany<ProductRedeemDateRestrictionSet>, IEnumerable<ProductRedeemDateRestrictionSet>>(_dbConnection, commandDefinition);

            return dbResult;
        }

        public async Task<int> DeleteDateRestriction(int ProductId, string UserName, IDbConnection _dbConnection)
        {
            CommandDefinition commandDefinition;
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@product_id", ProductId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@UserName", UserName, DbType.String, ParameterDirection.Input);
            parameters.Add("@Result", default(int), DbType.Int32, ParameterDirection.Output);

            commandDefinition = new CommandDefinition("product.sp_del_date_restriction", commandType: CommandType.StoredProcedure,
                                                    parameters: parameters, cancellationToken: default);
            var dbaffectedRows = await _dapperOperation.ProcessSql<ExecuteCommand, int>(_dbConnection, commandDefinition);
            int _result = parameters.Get<int>("@Result");
            return _result;
        }

        public async Task<bool> SendProductCreateRestrictionMessage(int TenantId, string queueNameConfig, CreateProductRestrictionV1 message)
        {
            try
            {
                return await _txcServiceBusSender.SendMessageAsync(TenantId, queueNameConfig, message, ESBMessageType.ProductRestriction, (int)ActionType.Create, "TXCProduct", 1);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SendProductDeleteRestrictionMessage(int TenantId, string queueNameConfig, CreateProductRestrictionV1 message)
        {
            try
            {
                return await _txcServiceBusSender.SendMessageAsync(TenantId, queueNameConfig, message, ESBMessageType.ProductRestriction, (int)ActionType.Delete, "TXCProduct", 1);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
