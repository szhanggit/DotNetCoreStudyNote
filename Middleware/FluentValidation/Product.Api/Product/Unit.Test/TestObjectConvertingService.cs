using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Infrastructure.Mapper;
using Services.Interface;
using System;
using System.Threading.Tasks;
using TXC.Proto.Product;
using Xunit;
using Services.Core;
using TXC.Common.MessageContract.Product;
using Domain.Models;

namespace Unit.Test
{
    public class TestObjectConvertingService : CommonHelper
    {
        [Fact]
        public void Test_ConvertProductConditionResponseToProductCondition_Case0()
        {
            ProductConditionResponse _response = new ProductConditionResponse {
                ProductId = 1,
                MinRedeemQuantity = 2,
                MaxIssuingQuantity = null,
                ValidFrom = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(1900, 01, 01), DateTimeKind.Utc)),
                ValidTo = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(3999, 01, 01), DateTimeKind.Utc)),
                ReverseLimitId = 2,
                PreAuthorizationExpiryInterval = 3,
                PreAuthorizationExpiryUnit = 2
            };

            ProductCondition _result = GetObjectConvertingService().ConvertProductConditionResponseToProductCondition(_response);
            Assert.Equal(1, _result.ProductId);
            Assert.Equal(2, _result.MinRedeemQuantity);
            Assert.Null(_result.MaxIssuingQuantity);
            Assert.Equal(new DateTime(1900, 01, 01), _result.ValidFrom);
            Assert.Equal(new DateTime(3999, 01, 01), _result.ValidTo);
            Assert.Equal(2, _result.ReverseLimitId);
            Assert.Equal(3, _result.PreAuthorizationExpiryInterval);
            Assert.Equal(2, _result.PreAuthorizationExpiryUnit);
        }

        [Fact]
        public void Test_ConvertProductUpdatingConditionRequestToProductCondition_Case0()
        {
            ProductUpdatingConditionRequest _response = new ProductUpdatingConditionRequest
            {
                ProductId = 1,
                MinRedeemQuantity = 2,
                MaxIssuingQuantity = null,
                ValidFrom = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(1900, 01, 01), DateTimeKind.Utc)),
                ValidTo = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(3999, 01, 01), DateTimeKind.Utc)),
                ReverseLimitId = 2,
                PreAuthorizationExpiryInterval = 3,
                PreAuthorizationExpiryUnit = 2
            };

            ProductCondition _result = GetObjectConvertingService().ConvertProductUpdatingConditionRequestToProductCondition(_response);

            Assert.Equal(1, _result.ProductId);
            Assert.Equal(2, _result.MinRedeemQuantity);
            Assert.Null(_result.MaxIssuingQuantity);
            Assert.Equal(new DateTime(1900, 01, 01), _result.ValidFrom);
            Assert.Equal(new DateTime(3999, 01, 01), _result.ValidTo);
            Assert.Equal(2, _result.ReverseLimitId);
            Assert.Equal(3, _result.PreAuthorizationExpiryInterval);
            Assert.Equal(2, _result.PreAuthorizationExpiryUnit);
        }


        [Fact]
        public void Test_ConvertProductUpdatingConditionRequestToUpdateProductConditionV1_Case0()
        {
            ProductUpdatingConditionRequest _response = new ProductUpdatingConditionRequest
            {
                ProductId = 1,
                MinRedeemQuantity = 2,
                MaxIssuingQuantity = null,
                ValidFrom = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(1900, 01, 01), DateTimeKind.Utc)),
                ValidTo = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(3999, 01, 01), DateTimeKind.Utc)),
                ReverseLimitId = 2,
                PreAuthorizationExpiryInterval = 3,
                PreAuthorizationExpiryUnit = 2
            };

            UpdateProductConditionV1 _result = GetObjectConvertingService().ConvertProductUpdatingConditionRequestToUpdateProductConditionV1(_response);

            Assert.Equal(1, _result.ProductId);
            Assert.Equal(2, _result.MinRedeemQuantity);
            Assert.Null(_result.MaxIssuingQuantity);
            Assert.Equal(new DateTime(1900, 01, 01), _result.ValidFrom);
            Assert.Equal(new DateTime(3999, 01, 01), _result.ValidTo);
            Assert.Equal(2, _result.ReverseLimitId);
            Assert.Equal(3, _result.PreAuthorizationExpiryInterval);
            Assert.Equal(2, _result.PreAuthorizationExpiryUnit);
        }

    }
}
