using Dapper;
using Domain.Dto;
using Domain.Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.CacheManagement;
using TXC.Common.Data;
using TXC.Common.Data.TenantDbConnection;
using TXC.Common.Domain;
using TXC.Common.MessageContract;
using TXC.Common.MessageContract.Product;
using TXC.Proto.Product;

namespace Services.Core
{
    public class CommonProductService : ICommonProductService
    {
        private readonly IDapperOperation _dapperOperation;
        private readonly ITX2ServiceBusSender _txcServiceBusSender;

        public CommonProductService()
        {

        }

        public CommonProductService(
            IDapperOperation dapperOperation
            , ITX2ServiceBusSender txcServiceBusSender)
        {
            _dapperOperation = dapperOperation;
            _txcServiceBusSender = txcServiceBusSender;
        }

        public async Task<bool> SendProductUpdateStatusMessage(int TenantId, string queueNameConfig, UpdateProductStatusV1 message)
        {
            try
            {
                return await _txcServiceBusSender.SendMessageAsync(TenantId, queueNameConfig, message, ESBMessageType.ProductStatus, (int)ActionType.Update, "TXCProduct", 1);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> UpdateProductStatus(UpdateProductStatusRequest request, IDbConnection _dbConnection)
        {
            CommandDefinition commandDefinition;
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@product_id", request.ProductId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Status", request.Status, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@UserName", request.TX2UserName, DbType.String, ParameterDirection.Input);
            parameters.Add("@Result", default(int), DbType.Int32, ParameterDirection.Output);

            commandDefinition = new CommandDefinition("product.sp_upd_status", commandType: CommandType.StoredProcedure,
                                                    parameters: parameters, cancellationToken: default);
            var dbaffectedRows = await _dapperOperation.ProcessSql<ExecuteCommand, int>(_dbConnection, commandDefinition);
            int _result = parameters.Get<int>("@Result");
            return _result;
        }

        public async Task<Tuple<int, int>> CheckProductId(int ProductId, IDbConnection _dbConnection)
        {
            CommandDefinition commandDefinition;
            DynamicParameters parameters = new DynamicParameters();
            Tuple<int, int> res = null;

            parameters.Add("@product_id", ProductId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Result", 0, DbType.Int32, ParameterDirection.Output);
            parameters.Add("@MultiSelectionType", 0, DbType.Int32, ParameterDirection.Output);

            commandDefinition = new CommandDefinition("product.sp_chk_product", commandType: CommandType.StoredProcedure,
                                                    parameters: parameters, cancellationToken: default);

            await _dapperOperation.ProcessSql<ExecuteCommandWithReturn<int>, int>(_dbConnection, commandDefinition);
            int result = parameters.Get<int>("@Result");
            int multiSelectionType = parameters.Get<int>("@MultiSelectionType");
            res = new Tuple<int, int>(result, multiSelectionType);
            return res;
        }

        public async Task<bool> CheckContract(int ProductId, IDbConnection _dbConnection)
        {
            CommandDefinition commandDefinition;
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@product_id", ProductId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Result", 0, DbType.Int32, ParameterDirection.Output);

            commandDefinition = new CommandDefinition("product.sp_chk_contract", commandType: CommandType.StoredProcedure,
                                                    parameters: parameters, cancellationToken: default);

            await _dapperOperation.ProcessSql<ExecuteCommandWithReturn<int>, int>(_dbConnection, commandDefinition);
            int result = parameters.Get<int>("@Result");
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckReverseLimitName(int ReverseLimitNameId, IDbConnection _dbConnection)
        {
            ProductConditionResponse _pcResponse = new ProductConditionResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@reverse_limit_name_id", ReverseLimitNameId, DbType.Int32, ParameterDirection.Input);

            CommandDefinition commandDefinition = new CommandDefinition(@"select 1 from [product].[tb_p_product_reverse_limit] with(nolock) 
                                                                        where reverse_limit_id = @reverse_limit_name_id", commandType: CommandType.Text,
                                                                        parameters: parameters, cancellationToken: default);

            int? dbResult = await _dapperOperation.ProcessSql<ExecuteCommandWithReturn<int>, int>(_dbConnection, commandDefinition);
            if (dbResult.HasValue && dbResult == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
