using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public interface IGetProductBrandService
    {
        Task<ProtoBaseResponse> GetProductBrand(
            GetProductBrandRequest request
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , GetProductBrandDel getProductBrand
            , CheckProductDel checkProduct);
    }
}
