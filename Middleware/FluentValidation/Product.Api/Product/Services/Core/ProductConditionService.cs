using AutoMapper;
using Dapper;
using Domain.Models;
using Google.Protobuf.WellKnownTypes;
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
    public class ProductConditionService : IProductConditionService
    {
        private readonly IDapperOperation _dapperOperation;
        private readonly ITX2ServiceBusSender _txcServiceBusSender;

        public ProductConditionService()
        {

        }

        public ProductConditionService(IDapperOperation dapperOperation, ITX2ServiceBusSender txcServiceBusSender)
        {
            _dapperOperation = dapperOperation;
            _txcServiceBusSender = txcServiceBusSender;
        }

        public async Task<ProductConditionResponse> GetProductCondition(int ProductId, IDbConnection _dbConnection)
        {
            ProductConditionResponse _pcResponse = new ProductConditionResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@product_id", ProductId, DbType.Int32, ParameterDirection.Input);

            CommandDefinition commandDefinition = new CommandDefinition(@"select product_id as ProductId, min_redeem_quantity as MinRedeemQuantity,
                                                                        max_issuing_quantity as MaxIssuingQuantity,
                                                                        valid_from as ValidFrom,
                                                                        valid_to as ValidTo,
                                                                        reverse_limit_id as ReverseLimitId,
                                                                        pre_authorization_expiry_interval as PreAuthorizationExpiryInterval,
                                                                        pre_authorization_expiry_unit as PreAuthorizationExpiryUnit 
                                                                        from [product].[tb_p_product] with(nolock) 
                                                                        where product_id = @product_id", commandType: CommandType.Text,
                                                                        parameters: parameters, cancellationToken: default);

            var dbResult = await _dapperOperation.ProcessSql<ExecuteCommandWithReturn<ProductCondition>, ProductCondition>(_dbConnection, commandDefinition);
            _pcResponse.ProductId = dbResult.ProductId;
            _pcResponse.MinRedeemQuantity = dbResult.MinRedeemQuantity;
            _pcResponse.MaxIssuingQuantity = dbResult.MaxIssuingQuantity;
            _pcResponse.ValidFrom = Timestamp.FromDateTime(DateTime.SpecifyKind(dbResult.ValidFrom, DateTimeKind.Utc));
            _pcResponse.ValidTo = Timestamp.FromDateTime(DateTime.SpecifyKind(dbResult.ValidTo, DateTimeKind.Utc));
            _pcResponse.ReverseLimitId = dbResult.ReverseLimitId;
            _pcResponse.PreAuthorizationExpiryInterval = dbResult.PreAuthorizationExpiryInterval;
            _pcResponse.PreAuthorizationExpiryUnit = dbResult.PreAuthorizationExpiryUnit;

            return _pcResponse;
        }

        public async Task<int> UpdateProductCondition(ProductUpdatingConditionRequest request, IDbConnection _dbConnection)
        {
            try
            {
                CommandDefinition commandDefinition;
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@product_id", request.ProductId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@UserName", request.TX2UserName, DbType.String, ParameterDirection.Input);
                parameters.Add("@min_redeem_quantity", request.MinRedeemQuantity, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@max_issuing_quantity", request.MaxIssuingQuantity, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@valid_from", request.ValidFrom.ToDateTime(), DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@valid_to", request.ValidTo.ToDateTime(), DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@reverse_limit_id", request.ReverseLimitId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@pre_authorization_expiry_interval", request.PreAuthorizationExpiryInterval, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@pre_authorization_expiry_unit", request.PreAuthorizationExpiryUnit, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Result", default(int), DbType.Int32, ParameterDirection.Output);
                commandDefinition = new CommandDefinition("product.sp_upd_condition", commandType: CommandType.StoredProcedure,
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

        public async Task<bool> SendProductConditionMessage(int TenantId, string queueNameConfig, UpdateProductConditionV1 message)
        {
            try
            {
                return await _txcServiceBusSender.SendMessageAsync(TenantId, queueNameConfig, message, ESBMessageType.ProductCondition, (int)ActionType.Update, "TXCProduct", 1);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CompareConditionIsTheSame(ProductCondition item1, ProductCondition item2)
        {
            if (item1 == null && item2 != null)
            {
                return false;
            }
            else if (item1 != null && item2 == null)
            {
                return false;
            }
            else if (item1 == null && item2 == null)
            {
                return true;
            }
            else
            {
                if (item1.MinRedeemQuantity == item2.MinRedeemQuantity 
                    && item1.MaxIssuingQuantity == item2.MaxIssuingQuantity
                    && item1.ValidFrom == item2.ValidFrom
                    && item1.ValidTo == item2.ValidTo
                    && item1.ReverseLimitId == item2.ReverseLimitId
                    && item1.PreAuthorizationExpiryInterval == item2.PreAuthorizationExpiryInterval
                    && item1.PreAuthorizationExpiryUnit == item2.PreAuthorizationExpiryUnit)
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
}
