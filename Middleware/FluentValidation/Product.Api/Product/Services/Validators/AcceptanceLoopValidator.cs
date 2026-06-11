using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Validators
{
    public class AcceptanceLoopValidator : AbstractValidator<UpdateProductAcceptanceLoopRequest>
    {
        public AcceptanceLoopValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;
            RuleFor(p => p.AcceptanceLoopId)
              .NotNull()
              .NotEmpty()
              .GreaterThanOrEqualTo(100).WithMessage("Invalid Acceptance Loop Id");
        }
    }
}
