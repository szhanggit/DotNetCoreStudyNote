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
    public class ProductAcceptanceLoopService : IProductAcceptanceLoopService
    {
        private readonly IDapperOperation _dapperOperation;

        public ProductAcceptanceLoopService()
        {
        }

        public ProductAcceptanceLoopService(
            IDapperOperation dapperOperation)
        {
            _dapperOperation = dapperOperation;
        }

        public async Task<Tuple<int, int>> CheckAcceptanceLoopId(int? AcceptanceLoopId, IDbConnection _dbConnection)
        {
            CommandDefinition commandDefinition;
            DynamicParameters parameters = new DynamicParameters();
            Tuple<int, int> res = null;

            if (!AcceptanceLoopId.HasValue)
            {
                res = new Tuple<int, int>(1, 1);
                return res;
            }

            parameters.Add("@acceptance_loop_id", AcceptanceLoopId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Result", 0, DbType.Int32, ParameterDirection.Output);
            parameters.Add("@Status", 0, DbType.Int32, ParameterDirection.Output);

            commandDefinition = new CommandDefinition("merchant_info.sp_chk_acceptance_loop", commandType: CommandType.StoredProcedure,
                                                    parameters: parameters, cancellationToken: default);

            await _dapperOperation.ProcessSql<ExecuteCommandWithReturn<int>, int>(_dbConnection, commandDefinition);
            int result = parameters.Get<int>("@Result");
            int status = parameters.Get<int>("@Status");
            res = new Tuple<int, int>(result, status);
            return res;
        }

        public async Task<int> UpdateProductAcceptanceLoop(UpdateProductAcceptanceLoopRequest request, IDbConnection _dbConnection)
        {
            CommandDefinition commandDefinition;
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@product_id", request.ProductId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@acceptance_loop_id", request.AcceptanceLoopId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@UserName", request.TX2UserName, DbType.String, ParameterDirection.Input);
            parameters.Add("@Result", default(int), DbType.Int32, ParameterDirection.Output);

            commandDefinition = new CommandDefinition("product.sp_upd_acceptance_loop", commandType: CommandType.StoredProcedure,
                                                    parameters: parameters, cancellationToken: default);
            var dbaffectedRows = await _dapperOperation.ProcessSql<ExecuteCommand, int>(_dbConnection, commandDefinition);
            int _result = parameters.Get<int>("@Result");
            return _result;
        }
    }
}
