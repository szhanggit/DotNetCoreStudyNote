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
    public class ExternalPropertyService : IExternalPropertyService
    {
        private readonly IDapperOperation _dapperOperation;
        private readonly ITX2ServiceBusSender _txcServiceBusSender;

        public ExternalPropertyService()
        {

        }

        public ExternalPropertyService(
            IDapperOperation dapperOperation
            , ITX2ServiceBusSender txcServiceBusSender)
        {
            _dapperOperation = dapperOperation;
            _txcServiceBusSender = txcServiceBusSender;
        }

        public async Task<int> InsertExternalProperty(List<ProductExternalPropertySet> _inputList, int ProductId, string UserName, IDbConnection _dbConnection)
        {
            try
            {
                CommandDefinition commandDefinition;
                DynamicParameters parameters = new DynamicParameters();

                var TypeExternalPropertyParameter = new List<SqlDataRecord>();
                var ExternalPropertyMetaData = new SqlMetaData[] {
                    new SqlMetaData("property_name", SqlDbType.NVarChar, 100),
                    new SqlMetaData("property_value", SqlDbType.NVarChar, 500),
                    new SqlMetaData("description", SqlDbType.NVarChar, 500)
                };

                foreach (var sc in _inputList)
                {
                    var record = new SqlDataRecord(ExternalPropertyMetaData);
                    record.SetValue(0, sc.PropertyName);
                    record.SetValue(1, sc.PropertyValue);
                    record.SetValue(2, sc.Description);
                    TypeExternalPropertyParameter.Add(record);
                }

                parameters.Add("@product_id", ProductId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@UserName", UserName, DbType.String, ParameterDirection.Input);
                parameters.Add("@ExternalPropertyList", TypeExternalPropertyParameter.AsTableValuedParameter("product.ExternalPropertyListType"));
                parameters.Add("@Result", default(int), DbType.Int32, ParameterDirection.Output);

                commandDefinition = new CommandDefinition("product.sp_ins_external_property", commandType: CommandType.StoredProcedure,
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

        public async Task<IEnumerable<ExternalPropertyItem>> GetExternalPropertyList(int ProductId, IDbConnection _dbConnection) 
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@product_id", ProductId, DbType.Int32, ParameterDirection.Input);

            CommandDefinition commandDefinition = new CommandDefinition("product.sp_sel_external_property", commandType: CommandType.StoredProcedure,
                                                                        parameters: parameters, cancellationToken: default);

            var dbResult = await _dapperOperation.ProcessSql<SelectMany<ExternalPropertyItem>, IEnumerable<ExternalPropertyItem>>(_dbConnection, commandDefinition);

            return dbResult;
        }

        public async Task<int> DeleteExternalProperty(int ProductId, string UserName, IDbConnection _dbConnection)
        {
            CommandDefinition commandDefinition;
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@product_id", ProductId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@UserName", UserName, DbType.String, ParameterDirection.Input);
            parameters.Add("@Result", default(int), DbType.Int32, ParameterDirection.Output);

            commandDefinition = new CommandDefinition("product.sp_del_external_property", commandType: CommandType.StoredProcedure,
                                                    parameters: parameters, cancellationToken: default);
            var dbaffectedRows = await _dapperOperation.ProcessSql<ExecuteCommand, int>(_dbConnection, commandDefinition);
            int _result = parameters.Get<int>("@Result");
            return _result;
        }

        public async Task<bool> SendProductCreateExternalPropertyMessage(int TenantId, string queueNameConfig, CreateProductExternalPropertyV1 message)
        {
            try
            {
                return await _txcServiceBusSender.SendMessageAsync(TenantId, queueNameConfig, message, ESBMessageType.ProductExternalProperty, (int)ActionType.Create, "TXCProduct", 1);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SendProductDeleteExternalPropertyMessage(int TenantId, string queueNameConfig, CreateProductExternalPropertyV1 message)
        {
            try
            {
                return await _txcServiceBusSender.SendMessageAsync(TenantId, queueNameConfig, message, ESBMessageType.ProductExternalProperty, (int)ActionType.Delete, "TXCProduct", 1);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
