using Azure.Messaging.ServiceBus;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Product.Api;
using Services.Core;
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
using Xunit;

namespace Unit.Test
{
    public class TestCommonProductService : CommonHelper
    {
        [Fact]
        public async Task Test_SendProductUpdateStatusMessage_Case0()
        {
            int tenantId = 1;
            string queueName = "tx2connectorqueue-gl-dev";
            var _updateProductStatusV1 = new UpdateProductStatusV1()
            {
                ProductId = 1,
                Status = 1,
                TenantId = 1,
                TX2UserName = "stzhang",

            };
            CommonProductService _commonProductService;
            ITX2ServiceBusSender _sender = InitServiceBus<CommonProductService>(out _commonProductService);
            bool flag = await _commonProductService.SendProductUpdateStatusMessage(tenantId, queueName, _updateProductStatusV1);
            Assert.True(flag);
        }

        [Fact]
        public async Task Test_CheckReverseLimitName_Case0()
        {
            int id = 4;

            CommonProductService _commonProductService;
            IDbConnection _dbConnection = InitDbConnection<CommonProductService>(out _commonProductService);
            bool flag = await _commonProductService.CheckReverseLimitName(id, _dbConnection);
            
            Assert.True(flag);
        }

        [Fact]
        public async Task Test_CheckReverseLimitName_Case1()
        {
            int id = 64;

            CommonProductService _commonProductService;
            IDbConnection _dbConnection = InitDbConnection<CommonProductService>(out _commonProductService);
            bool flag = await _commonProductService.CheckReverseLimitName(id, _dbConnection);

            Assert.False(flag);
        }
    }
}
