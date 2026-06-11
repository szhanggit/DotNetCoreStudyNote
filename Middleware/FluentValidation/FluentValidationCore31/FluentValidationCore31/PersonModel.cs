using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationCore31
{
    public class PersonModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class PersonModelValidator : AbstractValidator<PersonModel>
    {
        public PersonModelValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).Length(5);
        }
    }
}
