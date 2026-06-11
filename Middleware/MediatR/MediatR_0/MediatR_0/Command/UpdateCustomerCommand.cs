using MediatR;
using MediatR_0.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR_0.Command
{
    public class UpdateCustomerCommand : IRequest<Customer>
    {
        public Customer Customer { get; set; }
    }
}
