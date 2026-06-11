using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public interface IUpdateProductMemoService
    {
        Task<UpdateProductMemoResponse> UpdateProductBrand(
            UpdateProductMemoRequest request
            , CheckProductDel _checkProductDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , UpdateProductMemoDel _updateProductMemoDel
            , SendProductUpdateMemoMessageDel _sendProductUpdateMemoMessageDel
            );
    }
}
