using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.Domain;
using TXC.Common.Domain.Models.Pagination;

namespace TXC.Common.Services.Wrappers
{
    public interface IRequestListHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, Response<TOut>>
         where TIn : IRequestListWrapper<TOut>
         where TOut : IPaginationTotal
    {

    }
}
