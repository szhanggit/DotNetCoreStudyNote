using Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public interface IGetProductConditionService
    {
        Task<ProtoBaseResponse> GetProductCondition(
            ProductConditionRequest request
            , CheckProductDel _checkProductDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , GetProductConditionDel _getProductConditionDel
            );
    }
}
