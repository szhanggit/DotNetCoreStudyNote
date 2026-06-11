using FluentValidation;
using TXCFluValidation.Models;

namespace TXCFluValidation.Validators
{
    public class SKUValidator : AbstractValidator<SKU>
    {
        public SKUValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;
            RuleFor(p => p.ProductList).Must(noDuplicateAppTenant).WithMessage("duplicate product id and program id")
                .When(p => p.ProductList.Count() > 2 && p.ProductList.Count() < 5 && p.ProductList != null);
            RuleFor(p => p.AddressInfo).SetValidator(new AddressValidator());
        }

        bool noDuplicateAppTenant(IEnumerable<ProductDto> data)
        {
            var duplicates = data.GroupBy(x => new { x.Id, x.ProgramId })
                                        .Where(g => g.Count() > 1)
                                        .Select(x => x.Key).ToList();
            return duplicates.Count() == 0;
        }
    }

    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(p => p.AddressId)
                .NotNull()
                .NotEmpty()
                .NotEqual(0)
                .GreaterThan(1);
        }
    }
}
