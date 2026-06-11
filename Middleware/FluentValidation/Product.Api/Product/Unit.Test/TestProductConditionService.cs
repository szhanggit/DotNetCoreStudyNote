using AutoMapper;
using Azure.Messaging.ServiceBus;
using Domain.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Product.Api;
using Services.Core;
using Services.Interface;
using Services.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.CacheManagement;
using TXC.Common.CacheManagement.Resolver;
using TXC.Common.Data;
using TXC.Common.MessageContract;
using TXC.Common.MessageContract.Product;
using TXC.Common.Security.Cryptography;
using TXC.Proto.Product;
using Xunit;

namespace Unit.Test
{
    public class TestProductConditionService : CommonHelper
    {
        [Fact]
        public async Task Test_GetProductCondition_Case0()
        {
            ProductConditionService _conditionService;
            IDbConnection _dbConnection = InitDbConnection<ProductConditionService>(out _conditionService);
            int ProductId = 1;
            ProductConditionResponse _condition = await _conditionService.GetProductCondition(ProductId, _dbConnection);
        }

        [Fact]
        public async Task Test_UpdateProductCondition_Case0()
        {
            ProductConditionService _conditionService;
            IDbConnection _dbConnection = InitDbConnection<ProductConditionService>(out _conditionService);
            ProductUpdatingConditionRequest request = new ProductUpdatingConditionRequest { 
                ProductId = 1,
                TenantId = 1,
                TenantName = "Thailand",
                TX2UserName = "stzhang",
                MinRedeemQuantity = 10,
                MaxIssuingQuantity = 20,
                ValidFrom = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(1900, 1, 1), DateTimeKind.Utc)),
                ValidTo = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(3999, 1, 1), DateTimeKind.Utc)),
                ReverseLimitId = 10,
                PreAuthorizationExpiryInterval = 10,
                PreAuthorizationExpiryUnit = 2
            };
            var re = await _conditionService.UpdateProductCondition(request, _dbConnection);
            Assert.Equal(1, re);
        }

        [Fact]
        public async Task Test_SendProductDeleteRestrictionMessage_Case0()
        {
            int tenantId = 1;
            string queueName = "tx2connectorqueue-gl-dev";
            var _updateProductConditionV1 = new UpdateProductConditionV1()
            {
                ProductId = 1,
                TenantId = 1,
                TX2UserName = "stzhang",
                MinRedeemQuantity = 10,
                MaxIssuingQuantity = null,
                ValidFrom = new DateTime(1900, 1, 1),
                ValidTo = new DateTime(3999,12,31),
                ReverseLimitId = 2,
                PreAuthorizationExpiryInterval = 3,
                PreAuthorizationExpiryUnit = 10
            };
            ProductConditionService _productRestrictionService;
            ITX2ServiceBusSender _sender = InitServiceBus<ProductConditionService>(out _productRestrictionService);
            bool flag = await _productRestrictionService.SendProductConditionMessage(tenantId, queueName, _updateProductConditionV1);
            Assert.True(flag);
        }

        [Fact]
        public async Task Test_CompareConditionIsTheSame_Case0()
        {
            IProductConditionService _productConditionService = new ProductConditionService();
            ProductCondition item0 = new ProductCondition {
                ProductId = 1,
                MinRedeemQuantity = 10,
                MaxIssuingQuantity = null,
                ValidFrom = new DateTime(1900, 1, 1),
                ValidTo = new DateTime(3999, 12, 31),
                ReverseLimitId = 2,
                PreAuthorizationExpiryInterval = 3,
                PreAuthorizationExpiryUnit = 10
            };
            ProductCondition item1 = new ProductCondition
            {
                ProductId = 1,
                MinRedeemQuantity = 10,
                MaxIssuingQuantity = null,
                ValidFrom = new DateTime(1900, 1, 1),
                ValidTo = new DateTime(3999, 12, 31),
                ReverseLimitId = 2,
                PreAuthorizationExpiryInterval = 3,
                PreAuthorizationExpiryUnit = 10
            };
            bool _result = _productConditionService.CompareConditionIsTheSame(item0, item1);
            Assert.True(_result);
        }
    }
}
