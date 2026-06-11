using MediatR;
using MediatR_0.Data;
using MediatR_0.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_0.Query
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<Customer>>
    {
        private readonly IRepository<Customer> _repository;

        public GetCustomersQueryHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<List<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAll().ToList();
            //return await _repository.GetAll().ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
