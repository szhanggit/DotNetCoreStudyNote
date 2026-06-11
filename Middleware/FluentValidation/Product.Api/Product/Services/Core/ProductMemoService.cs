using Dapper;
using Domain.Models;
using Microsoft.SqlServer.Server;
using Services.Interface;
using Services.Utility.Helper;
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
    public class ProductMemoService : IProductMemoService
    {
        private readonly IDapperOperation _dapperOperation;
        private readonly ITX2ServiceBusSender _txcServiceBusSender;

        public ProductMemoService()
        {

        }

        public ProductMemoService(
            IDapperOperation dapperOperation
            , ITX2ServiceBusSender txcServiceBusSender)
        {
            _dapperOperation = dapperOperation;
            _txcServiceBusSender = txcServiceBusSender;
        }

        public async Task<int> UpdateProductMemo(UpdateProductMemoRequest request, IDbConnection _dbConnection)
        {
            try
            {
                CommandDefinition commandDefinition;
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@product_id", request.ProductId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@UserName", request.TX2UserName, DbType.String, ParameterDirection.Input);
                parameters.Add("@operation_note", request.OperationNote, DbType.String, ParameterDirection.Input);
                parameters.Add("@sales_note", request.SalesNote, DbType.String, ParameterDirection.Input);
                parameters.Add("@customer_service_note", request.CustomerServiceNote, DbType.String, ParameterDirection.Input);
                parameters.Add("@Result", default(int), DbType.Int32, ParameterDirection.Output);

                commandDefinition = new CommandDefinition("product.sp_upd_memo", commandType: CommandType.StoredProcedure,
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

        public async Task<ProductMemo> GetProductMemo(int ProductId, IDbConnection _dbConnection)
        {
            CommandDefinition commandDefinition;
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@product_id", ProductId, DbType.Int32, ParameterDirection.Input);

            commandDefinition = new CommandDefinition("product.sp_sel_memo", commandType: CommandType.StoredProcedure,
                                                                        parameters: parameters, cancellationToken: default);
            ProductMemo _memo = await _dapperOperation.ProcessSql<ExecuteCommandWithReturn<ProductMemo>, ProductMemo>(_dbConnection, commandDefinition);
            return _memo;
        }

        public async Task<bool> SendProductUpdateMemoMessage(int TenantId, string queueNameConfig, UpdateProductMemoV1 message)
        {
            try
            {
                return await _txcServiceBusSender.SendMessageAsync(TenantId, queueNameConfig, message, ESBMessageType.ProductMemo, (int)ActionType.Update, "TXCProduct", 1);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
