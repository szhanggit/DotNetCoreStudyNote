using Dapper;
using Services.Enums;
using Services.Extensions.Primitive;
using Services.Interface;
using Services.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.Data;
using TXC.Common.Data.TenantDbConnection;
using TXC.Proto.Product;
using TXC.Proto.Tenant;

namespace Services.Core
{
    public class UpdateProductPriceService : IUpdateProductPriceService
    {
        private IDbConnection _dbConnection;
        private readonly IDapperOperation _dapperOperation;
        private readonly ITenantDbConnection _tenantDbConnection;
        private readonly Tenant.TenantClient _tenantClient;
        public UpdateProductPriceService(IDbConnection dbConnection, IDapperOperation dapperOperation,
            ITenantDbConnection tenantDbConnection, Tenant.TenantClient tenantClient)
        {
            _dbConnection = dbConnection;
            _dapperOperation = dapperOperation;
            _tenantDbConnection = tenantDbConnection;
            _tenantClient = tenantClient;
        }
        public async Task<UpdateProductPriceResponse> UpdateProductPrice(UpdateProductPriceRequest request)
        {
            try
            {
                // initialize db connection
                var conn = await _tenantDbConnection.GetTenantDbConnection(request.TenantId.ToString(), false, default);
                if (!conn.Success)
                    return new UpdateProductPriceResponse() { Success = false, Message = "Error in Tenant DB" };

                _dbConnection = conn.Data;
                SqlMapper.ResetTypeHandlers();
                SqlMapper.AddTypeHandler(new ProtobufTimestampHandler());
                SqlMapper.AddTypeHandler(new ProtoBufDecimalHandler());

                if (request.ProductId <=0)
                {
                    return new UpdateProductPriceResponse() { Success = false, Message = "ProductId required" };
                }
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ProductId", request.ProductId, DbType.Int32, ParameterDirection.Input);
                CommandDefinition commandDefinition = new CommandDefinition("product.sp_sel_product_by_id", commandType: CommandType.StoredProcedure, parameters: parameters);

                var productresult = await _dapperOperation.ProcessSql<SelectMany<ProductBasicInfo>, IEnumerable<ProductBasicInfo>>(_dbConnection, commandDefinition);
                if (productresult.Count() == 0)
                {
                    return new UpdateProductPriceResponse() { Success = false, Message = "Product not found" };
                }
                int productType = productresult.FirstOrDefault().ProductType;

                //validate parameters
                var val = await ValidateParams(request, productType);
                if (!val.Success)
                {
                    return val;
                }
                var tenantInfo = _tenantClient.GetTenantById(new GetTenantByIdRequest() { TenantBasicInfoId = request.TenantId });
                GetTenantByIdResponse packedData = tenantInfo.Data == null ? null : tenantInfo.Data.Unpack<GetTenantByIdResponse>();
                decimal taxrate = (decimal)packedData.CompanyTaxRate;


                if (productType ==(int)ProductTypeEnum.ProductBased)
                {
                    parameters = new DynamicParameters();
                    parameters.Add("@ProductId", request.ProductId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@FaceValue", Convert.ToInt32(CalculateWithoutTax(request.FaceValueWithTax.Value, taxrate)), DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@FaceValueWithTax", request.FaceValueWithTax.Value, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@SellingPricePrepaid", CalculateWithoutTax(request.SellingPricePrepaidWithTax.ToDecimal(), taxrate), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@SellingPricePrepaidWithTax", request.SellingPricePrepaidWithTax.ToDecimal(), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@SellingPricePostpaid", CalculateWithoutTax(request.SellingPricePostpaidWithTax.ToDecimal(), taxrate), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@SellingPricePostpaidWithTax", request.SellingPricePostpaidWithTax.ToDecimal(), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@Cost", CalculateWithoutTax(request.CostWithTax.ToDecimal(), taxrate), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@CostWithTax", request.CostWithTax.ToDecimal(), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@Balance", request.Balance, DbType.Int32, ParameterDirection.Input);

                    //null paramters
                    parameters.Add("@CustomerFeePrepaid", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@CustomerFeePrepaidWithTax", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@CustomerFeePostpaid", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@CustomerFeePostpaidWithTax", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@MerchantFee", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@MerchantFeeWithTax", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@DefaultSellingPrice", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@DefaultProductCost", null, DbType.Decimal, ParameterDirection.Input);


                }
                else if (productType ==(int)ProductTypeEnum.DynamicFaceValue)
                {
                    parameters = new DynamicParameters();
                    parameters.Add("@ProductId", request.ProductId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@DefaultSellingPrice", request.DefaultSellingPrice.ToDecimal(), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@DefaultProductCost", request.DefaultProductCost.ToDecimal(), DbType.Decimal, ParameterDirection.Input);

                    //null parameters
                    parameters.Add("@FaceValue", null, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@FaceValueWithTax", null, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@SellingPricePrepaid", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@SellingPricePrepaidWithTax", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@SellingPricePostpaid", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@SellingPricePostpaidWithTax", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@Cost", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@CostWithTax", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@Balance", null, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@CustomerFeePrepaid", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@CustomerFeePrepaidWithTax", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@CustomerFeePostpaid", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@CustomerFeePostpaidWithTax", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@MerchantFee", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@MerchantFeeWithTax", null, DbType.Decimal, ParameterDirection.Input);


                }
                else if (productType ==(int)ProductTypeEnum.ValueBased)
                {
                    parameters = new DynamicParameters();
                    parameters.Add("@ProductId", request.ProductId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@FaceValue", Convert.ToInt32(CalculateWithoutTax(request.FaceValueWithTax.Value, taxrate)), DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@FaceValueWithTax", request.FaceValueWithTax.Value, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@SellingPricePrepaid", CalculateWithoutTax(request.SellingPricePrepaidWithTax.ToDecimal(), taxrate), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@SellingPricePrepaidWithTax", request.SellingPricePrepaidWithTax.ToDecimal(), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@SellingPricePostpaid", CalculateWithoutTax(request.SellingPricePostpaidWithTax.ToDecimal(), taxrate), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@SellingPricePostpaidWithTax", request.SellingPricePostpaidWithTax.ToDecimal(), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@CustomerFeePrepaid", CalculateWithoutTax(request.CustomerFeePrepaidWithTax.ToDecimal(), taxrate), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@CustomerFeePrepaidWithTax", request.CustomerFeePrepaidWithTax.ToDecimal(), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@CustomerFeePostpaid", CalculateWithoutTax(request.CustomerFeePostpaidWithTax.ToDecimal(), taxrate), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@CustomerFeePostpaidWithTax", request.CustomerFeePostpaidWithTax.ToDecimal(), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@MerchantFee", CalculateWithoutTax(request.MerchantFeeWithTax.ToDecimal(), taxrate), DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@MerchantFeeWithTax", request.MerchantFeeWithTax.ToDecimal(), DbType.Decimal, ParameterDirection.Input);

                    //null parameters
                    parameters.Add("@Cost", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@CostWithTax", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@Balance", null, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@DefaultSellingPrice", null, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@DefaultProductCost", null, DbType.Decimal, ParameterDirection.Input);


                }
                commandDefinition = new CommandDefinition("product.sp_upd_product_price", commandType: CommandType.StoredProcedure, parameters: parameters);
                await _dapperOperation.ProcessSql<ExecuteCommand, int>(_dbConnection, commandDefinition);


                return new UpdateProductPriceResponse
                {
                    Success = true,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new UpdateProductPriceResponse
                {
                    Success = true,
                    Message = ex.Message
                };
            }
        }
        internal async Task<UpdateProductPriceResponse> ValidateParams(UpdateProductPriceRequest requestparam, int productType)
        {
            UpdateProductPriceResponse returnvalue = new UpdateProductPriceResponse();
            string missingparam = "";
            switch (productType)
            {
                case (int)ProductTypeEnum.ProductBased:
                    if (requestparam.FaceValueWithTax < 0 || requestparam.CostWithTax.ToDecimal() < 0 || requestparam.Balance < 0)
                        missingparam = "Invalid product price";
                    break;
                case (int)ProductTypeEnum.ValueBased:
                    if (requestparam.MerchantFeeWithTax.ToDecimal() < 0 || requestparam.FaceValueWithTax < 0)
                        missingparam =  "Invalid product price";
                    break;
                case (int)ProductTypeEnum.DynamicFaceValue:
                    if (requestparam.DefaultSellingPrice.ToDecimal() < 0 || requestparam.DefaultProductCost.ToDecimal() < 0)
                        missingparam = "Invalid product price";
                    break;
                case (int)ProductTypeEnum.SuperVoucher:
                    break;
                case (int)ProductTypeEnum.SmartBooklet:
                    break;
                case (int)ProductTypeEnum.SmartChoiceVoucher:
                    break;
            }
            if (!string.IsNullOrEmpty(missingparam))
            {
                return new UpdateProductPriceResponse() { Success = false, Message =  missingparam };
            }
            returnvalue.Success = true;

            return returnvalue;
        }

        internal decimal CalculateWithoutTax(decimal valueWithtax, decimal taxrate)
        {
            decimal valueWithoutTax = 0;
            //getting tax rate
            valueWithoutTax = valueWithtax / taxrate;

            return valueWithoutTax;
        }
    }
}
