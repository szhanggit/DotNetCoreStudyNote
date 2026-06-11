using FluentValidation.TestHelper;
using Services.Validators;
using System;
using Xunit;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var request = new TXC.Proto.Product.UpdateProductAcceptanceLoopRequest
            {
                TenantId = 9,
                TenantName = "GL",
                TX2UserName = "jgao",
                ProductId = 1,
                AcceptanceLoopId = 0
            };
            var validator = new AcceptanceLoopValidator();
            var result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(p => p.AcceptanceLoopId);
        }
    }
}
