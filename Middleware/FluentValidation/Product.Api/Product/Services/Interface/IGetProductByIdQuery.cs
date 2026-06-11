using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public interface IGetProductByIdQuery
    {
        public  Task<ProtoBaseResponse> GetProductById(GetProductByIdRequest request);

    }
}
