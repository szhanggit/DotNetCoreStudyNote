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
    public class ProductBrandService : IProductBrandService
    {
        private readonly IDapperOperation _dapperOperation;
        private readonly ITX2ServiceBusSender _txcServiceBusSender;

        public ProductBrandService()
        {

        }

        public ProductBrandService(
            IDapperOperation dapperOperation
            , ITX2ServiceBusSender txcServiceBusSender)
        {
            _dapperOperation = dapperOperation;
            _txcServiceBusSender = txcServiceBusSender;
        }

        public async Task<Tuple<int, int>> CheckBrandId(int? BrandId, IDbConnection _dbConnection)
        {
            CommandDefinition commandDefinition;
            DynamicParameters parameters = new DynamicParameters();
            Tuple<int, int> res = null;

            if (!BrandId.HasValue)
            {
                res = new Tuple<int, int>(1, 1);
                return res;
            }

            parameters.Add("@brand_id", BrandId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Result", 0, DbType.Int32, ParameterDirection.Output);
            parameters.Add("@Status", 0, DbType.Int32, ParameterDirection.Output);

            commandDefinition = new CommandDefinition("merchant_info.sp_chk_brand", commandType: CommandType.StoredProcedure,
                                                    parameters: parameters, cancellationToken: default);

            await _dapperOperation.ProcessSql<ExecuteCommandWithReturn<int>, int>(_dbConnection, commandDefinition);
            int result = parameters.Get<int>("@Result");
            int status = parameters.Get<int>("@Status");
            res = new Tuple<int, int>(result, status);
            return res;
        }

        public async Task<bool> SendProductUpdateBrandMessage(int TenantId, string queueNameConfig, UpdateProductBrandV1 message)
        {
            try
            {
                return await _txcServiceBusSender.SendMessageAsync(TenantId, queueNameConfig, message, ESBMessageType.ProductBrand, (int)ActionType.Update, "TXCProduct", 1);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> UpdateProductBrand(UpdateProductBrandRequest request, IDbConnection _dbConnection)
        {
            CommandDefinition commandDefinition;
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@product_id", request.ProductId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@brand_id", request.BrandId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@UserName", request.TX2UserName, DbType.String, ParameterDirection.Input);
            parameters.Add("@Result", default(int), DbType.Int32, ParameterDirection.Output);

            commandDefinition = new CommandDefinition("product.sp_upd_brand", commandType: CommandType.StoredProcedure,
                                                    parameters: parameters, cancellationToken: default);
            var dbaffectedRows = await _dapperOperation.ProcessSql<ExecuteCommand, int>(_dbConnection, commandDefinition);
            int _result = parameters.Get<int>("@Result");
            return _result;
        }

        public async Task<BrandDto> GetProductBrand(GetProductBrandRequest request, IDbConnection _dbConnection)
        {
            CommandDefinition commandDefinition;
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@product_id", request.ProductId, DbType.Int32, ParameterDirection.Input);

            commandDefinition = new CommandDefinition("product.sp_get_brand_by_product_id", commandType: CommandType.StoredProcedure,
                                                                        parameters: parameters, cancellationToken: default);
            BrandDto _brand = await _dapperOperation.ProcessSql<ExecuteCommandWithReturn<BrandDto>, BrandDto>(_dbConnection, commandDefinition);
            return _brand;
        }


    }
}
