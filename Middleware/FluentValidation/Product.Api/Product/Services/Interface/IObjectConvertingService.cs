using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.MessageContract.Product;
using TXC.Proto.Product;

namespace Services.Interface
{
    public delegate ProductCondition ConvertProductConditionResponseToProductConditionDel(ProductConditionResponse _productConditionResponse);
    public delegate ProductCondition ConvertProductUpdatingConditionRequestToProductConditionDel(ProductUpdatingConditionRequest _productUpdatingConditionRequest);
    public delegate UpdateProductConditionV1 ConvertProductUpdatingConditionRequestToUpdateProductConditionV1Del(ProductUpdatingConditionRequest _productUpdatingConditionRequest);
    public interface IObjectConvertingService
    {
        ProductCondition ConvertProductConditionResponseToProductCondition(ProductConditionResponse _productConditionResponse);
        ProductCondition ConvertProductUpdatingConditionRequestToProductCondition(ProductUpdatingConditionRequest _productUpdatingConditionRequest);
        UpdateProductConditionV1 ConvertProductUpdatingConditionRequestToUpdateProductConditionV1(ProductUpdatingConditionRequest _productUpdatingConditionRequest);
    }
}
