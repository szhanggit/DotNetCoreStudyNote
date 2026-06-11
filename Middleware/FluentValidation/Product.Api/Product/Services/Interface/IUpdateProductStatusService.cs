using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public interface IUpdateProductStatusService
    {
        Task<UpdateProductStatusResponse> UpdateProductStatus(
            UpdateProductStatusRequest request
            , SendProductUpdateStatusMessageDel _sendProductUpdateStatusMessageDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , UpdateProductStatusDel updateProductStatus
            , CheckProductDel checkProduct
            , CheckContractDel checkContract);
    }
}
