using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.Domain;

namespace TXC.Common.Services.Wrappers
{
    public interface IRequestWrapper<T> : IRequest<Response<T>>
    {
    }
}
