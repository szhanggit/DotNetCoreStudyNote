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
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly IRepository<Customer> _repository;

        public CreateCustomerCommandHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(request.Customer);
        }
    }
}
