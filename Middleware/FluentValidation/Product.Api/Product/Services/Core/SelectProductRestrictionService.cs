using Domain.Models;
using Google.Protobuf.WellKnownTypes;
using Services.Interface;
using Services.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.Domain;
using TXC.Proto.Product;

namespace Services.Core
{
    public class SelectProductRestrictionService : ISelectProductRestrictionService
    {
        private IDbConnection _dbConnection;
        public SelectProductRestrictionService()
        {

        }
        public async Task<ProtoBaseResponse> GetProductRedeemRestriction(
            GetRedeemRestrictionRequest request
            , CheckProductDel _checkProductDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , GetDateRestrictionListDel _getDateRestrictionListDel
            , GetTimeRestrictionListDel _getTimeRestrictionListDel
            , ConvertToDateRestrictionItemListDel _convertToDateRestrictionItemListDel
            , SerializeRestrictionTimeListDel _serializeRestrictionTimeListDel
            , ConvertToTimeRestrictionItemListDel _convertToTimeRestrictionItemListDel
            )
        {
            GetRedeemRestrictionResponse response = new GetRedeemRestrictionResponse();
            IEnumerable<ProductRedeemDateRestrictionSet> _dateRestrictionList = new List<ProductRedeemDateRestrictionSet>();
            List<RedeemDateRestrictionItem> _dateRestrictionItemList = new List<RedeemDateRestrictionItem>();
            IEnumerable<ProductRedeemTimeRestrictionSet> _timeRestrictionList = new List<ProductRedeemTimeRestrictionSet>();
            List<RedeemTimeRestrictionItem> _timeRestrictionItemList = new List<RedeemTimeRestrictionItem>();
            List<string> _timeRestrictionStringList = new List<string>();

            if (string.IsNullOrEmpty(request.TenantName))
                return new ProtoBaseResponse() { Success = false, Message = "TenantName header required" };

            if (request.ProductId <= 0)
                return new ProtoBaseResponse() { Success = false, Message = "Invalid request" };

            //check tx2 connector config
            TenantConfig queueNameConfig = await getConfig("TX2ConnectorQueueName", request.TenantId);

            // initialize db connection
            Response<IDbConnection> conn = await getDBConnection(request.TenantId);

            if (!conn.Success)
                return new ProtoBaseResponse() { Success = false, Message = "Error in Tenant DB" };

            _dbConnection = conn.Data;

            Tuple<int, int> productChecker = await _checkProductDel(request.ProductId, _dbConnection);
            if (productChecker.Item1 == -1)
            {
                return new ProtoBaseResponse() { Success = false, Message = "Product not found" };
            }

            _dateRestrictionList = await _getDateRestrictionListDel(request.ProductId, _dbConnection);
            _dateRestrictionItemList = await _convertToDateRestrictionItemListDel(_dateRestrictionList);

            _timeRestrictionList = await _getTimeRestrictionListDel(request.ProductId, _dbConnection);
            _timeRestrictionStringList = await _serializeRestrictionTimeListDel(_timeRestrictionList);
            _timeRestrictionItemList = await _convertToTimeRestrictionItemListDel(_timeRestrictionList, _timeRestrictionStringList);

            response.DateRestrictionItems.AddRange(_dateRestrictionItemList);
            response.TimeRestrictionItems.AddRange(_timeRestrictionItemList);
            response.ProductId = 1;

            return new ProtoBaseResponse
            {
                Success = true,
                Message = "Success",
                Data = Any.Pack(response)
            };
        }
    }
}
