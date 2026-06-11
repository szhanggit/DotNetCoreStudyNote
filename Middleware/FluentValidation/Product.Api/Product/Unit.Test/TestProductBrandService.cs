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
    public class TestProductBrandService : CommonHelper
    {
        [Fact]
        public async Task Test_InsertTimeRestriction_Case0()
        {
            ProductBrandService _productBrandService;
            IDbConnection _dbConnection = InitDbConnection<ProductBrandService>(out _productBrandService);
            Tuple<int,int> result = await _productBrandService.CheckBrandId(43, _dbConnection);
        }

        [Fact]
        public async Task Test_SendProductUpdateBrandMessage_Case0()
        {
            int tenantId = 1;
            string queueName = "tx2connectorqueue-gl-dev";
            var _updateProductBrandV1 = new UpdateProductBrandV1()
            {
                ProductId = 1,
                BrandId = 43,
                TenantId = 1,
                TX2UserName = "stzhang",

            };
            ProductBrandService _productBrandService;
            ITX2ServiceBusSender _sender = InitServiceBus<ProductBrandService>(out _productBrandService);
            bool flag = await _productBrandService.SendProductUpdateBrandMessage(tenantId, queueName, _updateProductBrandV1);
            Assert.True(flag);
        }

        [Fact]
        public async Task Test_SendProductUpdateBrandMessage_Case1()
        {
            string connectionString = "Endpoint=sb://esg-txcn-asse-bus-d.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Q+9w0tuFMJgA7EFvveBfnPER2sI/KkvqAa6THJpiJqg=";
            string queueName = "tx2connectorqueue-gl-dev";
            int tenantId = 1;
            await using var client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(queueName);

            var actionMessage = CreateMessageV1(tenantId);
            var serializedMessage = JsonConvert.SerializeObject(actionMessage);
            SBMessage sbMessage = new SBMessage()
            {
                MessageBody = serializedMessage,
                MessageVersion = 1,
                MessageType = ESBMessageType.ProductBrand,
                Publisher = "Transaction",
                PublishTime = DateTime.UtcNow,
                MessageHash = Hash.ComputeSha256Hash(serializedMessage)
            };

            ServiceBusMessage message = new ServiceBusMessage(JsonConvert.SerializeObject(sbMessage));

            await sender.SendMessageAsync(message);
        }

        private ActionMessageV1 CreateMessageV1(int tenantId)
        {
            var _updateProductBrandV1 = new UpdateProductBrandV1()
            {
                ProductId = 1,
                BrandId = 43,
                TenantId = 1,
                TX2UserName = "stzhang",
                
            };

            var actionMessage = new ActionMessageV1() { ActionBody = JsonConvert.SerializeObject(_updateProductBrandV1), ActionType = (int)ActionType.Update, TenantId = tenantId };
            return actionMessage;
        }
    }
}
