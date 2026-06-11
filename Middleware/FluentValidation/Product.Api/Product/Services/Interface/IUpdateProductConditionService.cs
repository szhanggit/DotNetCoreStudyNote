using Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public interface IUpdateProductConditionService
    {
        Task<ProductUpdatingConditionResponse> UpdateProductCondition(
            ProductUpdatingConditionRequest request
            , CheckProductDel _checkProductDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , UpdateProductConditionDel _updateProductConditionDel
            , SendProductConditionMessageDel _sendProductConditionMessageDel
            , CompareConditionIsTheSameDel _compareConditionIsTheSameDel
            , GetProductConditionDel _getProductConditionDel
            , CheckReverseLimitNameDel _checkReverseLimitNameDel
            , ConvertProductConditionResponseToProductConditionDel _convertProductConditionResponseToProductConditionDel
            , ConvertProductUpdatingConditionRequestToProductConditionDel _convertProductUpdatingConditionRequestToProductConditionDel
            , ConvertProductUpdatingConditionRequestToUpdateProductConditionV1Del _convertProductUpdatingConditionRequestToUpdateProductConditionV1Del
            );
    }
}
