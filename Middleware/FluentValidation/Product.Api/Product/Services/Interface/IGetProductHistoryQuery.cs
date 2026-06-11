
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public interface IGetProductHistoryQuery
    {
        public Task<ProtoBaseResponse> GetProductHistory(GetProductHistoryRequest request);
    }
}