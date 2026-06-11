using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public interface IGetProductExternalPropertyService
    {
        Task<ProtoBaseResponse> GetProductExternalProperty(
            GetProductExternalPropertyRequest request
            , CheckProductDel _checkProductDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , GetExternalPropertyListDel _getExternalPropertyListDel
            );
    }
}
