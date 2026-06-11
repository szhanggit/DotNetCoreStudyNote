using Services.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public interface ISelectProductRestrictionService
    {
        Task<ProtoBaseResponse> GetProductRedeemRestriction(
            GetRedeemRestrictionRequest request
            , CheckProductDel _checkProductDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , GetDateRestrictionListDel _getDateRestrictionListDel
            , GetTimeRestrictionListDel _getTimeRestrictionListDel
            , ConvertToDateRestrictionItemListDel _convertToDateRestrictionItemListDel
            , SerializeRestrictionTimeListDel _serializeRestrictionTimeListDel
            , ConvertToTimeRestrictionItemListDel _convertToTimeRestrictionItemListDel
            );
    }
}
