using MediatR;
using MediatR_0.Data;
using MediatR_0.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_0.Command
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly IRepository<Customer> _repository;

        public UpdateCustomerCommandHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.UpdateAsync(request.Customer);

            return customer;
        }
    }
}
