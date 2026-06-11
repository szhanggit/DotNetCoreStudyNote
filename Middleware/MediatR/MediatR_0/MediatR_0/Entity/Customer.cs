using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR_0.Entity
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public int? Age { get; set; }

        public string Phone { get; set; }
    }
}
