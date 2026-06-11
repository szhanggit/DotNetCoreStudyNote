using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public delegate Task<Tuple<int, int>> CheckAcceptanceLoopDel(int? AcceptanceLoopId, IDbConnection _dbConnection);
    public delegate Task<int> UpdateProductAcceptanceLoopDel(UpdateProductAcceptanceLoopRequest request, IDbConnection _dbConnection);

    public interface IProductAcceptanceLoopService
    {
        Task<Tuple<int, int>> CheckAcceptanceLoopId(int? AcceptanceLoopId, IDbConnection _dbConnection);

        Task<int> UpdateProductAcceptanceLoop(UpdateProductAcceptanceLoopRequest request, IDbConnection _dbConnection);

    }
}
