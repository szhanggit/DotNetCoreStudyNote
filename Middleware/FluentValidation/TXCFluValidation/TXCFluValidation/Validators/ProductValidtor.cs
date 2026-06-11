using FluentValidation;
using TXCFluValidation.Models;

namespace TXCFluValidation.Validators
{
    public class ProductValidtor : AbstractValidator<ProductDto>
    {
        public ProductValidtor()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;//CascadeMode.Stop;
            RuleFor(p => p.Id)
               .NotNull().WithMessage("Product id cannot be null.")
               .NotEmpty().WithMessage("Product id cannot be empty.")
               .NotEqual(0).WithMessage("Product id cannot be zero.")
               .GreaterThan(0).WithMessage("Product id must be greater than zero.")
               .InclusiveBetween(0, 500);

            RuleFor(p => p.Name)
                .NotNull().WithMessage("Product name cannot be null.")
                .NotEmpty().WithMessage("Product name cannot be empty.")
                .MinimumLength(3).WithMessage("The length of product name cannot be less than 3.")
                .MaximumLength(5).WithMessage("The length of product name cannot be greater than 5.")                
                .Must(p => p.Count() > 2);

            RuleFor(p => p.ProductCode)
                .NotNull().WithMessage("Product code cannot be null.")
                .NotEmpty().WithMessage("Product code cannot be empty.")
                .Length(6).WithMessage("The length of product code must be 6.");

            RuleFor(p => p.ProgramId)
                .NotNull().WithMessage("Program id cannot be null.")
                .NotEmpty().WithMessage("Program id cannot be empty.")
                .GreaterThan(10).WithMessage("Program id must be greater than 10.")
                .GreaterThanOrEqualTo(10);

            RuleFor(p => p.Type)
                .NotEmpty()
                .NotNull()
                .IsInEnum().WithMessage("Invalid product type.");

            RuleFor(p => p.Status)
                .Must(x => x == false || x == true)
                .NotNull();

            RuleFor(p => p.Created)
                .LessThanOrEqualTo(DateTime.Now.Date);

            RuleForEach(p => p.SupplierList)
                .SetValidator(new SupplierValidator());
            /*
            RuleForEach(r => r.AcceptanceLoopMerchants)
                    .ChildRules(c =>
                    {
                        c.RuleFor(r => r.MerchantId)
                            .NotNull()
                            .NotEmpty()
                            .GreaterThan(0);
                        c.RuleFor(r => r.Status)
                            .NotNull()
                            .NotEmpty();
                        c.RuleFor(r => r.AcceptanceLoopMerchantShops.Count)
                            .GreaterThan(0);
                        c.RuleFor(r => r.AcceptanceLoopMerchantShops)
                            .Must(x => x.GroupBy(x => x.ShopId).Count() > 0)
                            .WithMessage("Duplicate shop found.");
                        c.RuleForEach(r => r.AcceptanceLoopMerchantShops)
                            .ChildRules(c =>
                            {
                                c.RuleFor(r => r.ShopId)
                                    .NotNull()
                                    .NotEmpty()
                                    .GreaterThan(0);
                                c.RuleFor(r => r.Status)
                                    .NotNull()
                                    .NotEmpty();
                            });
                    });             
             */
        }
    }
}


/*
            RuleFor(p => p.Image)
                .NotEmpty()
                .NotNull()
                .Must(p => p.Length <= 1000000).WithMessage("Max file size is at least 1 MB")
                .Must(p => p.ContentType == "image/jpeg"
                        || p.ContentType == "image/png"
                        || p.ContentType == "image/bmp"
                        || p.ContentType == "image/jpg")
                .WithMessage("Invalid image type");				
												
            RuleFor(p => p.FileName)
                .NotEmpty()
                .NotNull()
                .Must(p => Path.HasExtension(p.ToString()) == false)
                .WithMessage("File name must not have extension");	

            RuleFor(p => p.DisplayVoucherNumberUnderBarcode)
                           .Must(x => x == false || x == true)
                            .When(p => p.DisplayVoucherNumberUnderBarcode != null);


https://stackoverflow.com/questions/27536384/regular-expression-issue-with-fluent-validation
RuleFor(x => x.FirstName)              
 .NotEmpty().WithLocalizedMessage(ResourceAreas.Messages.Message_PersonalDetails_1001_firstname)
 .Length(1, 
     20).WithLocalizedMessage(ResourceAreas.Messages.Message_Onboarding_100006_maxlength) 
 .Matches(@"^[a-zA-Z-']$").WithLocalizedMessage(ResourceAreas.Messages.Message_Onboarding_PersonalDetails_100007_validname);

RuleFor(customer => customer.Password).Equal(customer => customer.PasswordConfirmation);

https://docs.fluentvalidation.net/en/latest/built-in-validators.html
RuleFor(x => x.ErrorLevelName).IsEnumName(typeof(ErrorLevel), caseSensitive: false);


            RuleFor(r => r.AcceptanceLoopMerchants)
                .Must(x => x.GroupBy(x => x.MerchantId).Count() > 0)
                .WithMessage("Duplicate merchant found.");



 */


public class SupplierValidator : AbstractValidator<Supplier>
{
    public SupplierValidator()
    {
        CascadeMode = CascadeMode.Continue;

        RuleFor(p => p.SupplierId)
            .NotEmpty()
            .NotNull();
        RuleFor(p => p.SupplierName)
            .NotEmpty()
            .NotNull();
        RuleFor(p => p.ContactNumber)
            .NotEmpty()
            .NotNull()
            .IsInEnum();
        RuleFor(p => p.ContactEmail)
            .NotEmpty()
            .NotNull()
            .EmailAddress();

    }
}