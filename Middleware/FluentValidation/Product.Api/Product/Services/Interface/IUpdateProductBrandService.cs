using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public interface IUpdateProductBrandService
    {
        Task<UpdateProductBrandResponse> UpdateProductBrand(
            UpdateProductBrandRequest request
            , CheckBrandDel checkBrandDel
            , SendProductUpdateBrandMessageDel _sendProductUpdateBrandMessageDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , UpdateProductBrandDel updateProductBrand
            , CheckProductDel checkProduct);
    }
}
