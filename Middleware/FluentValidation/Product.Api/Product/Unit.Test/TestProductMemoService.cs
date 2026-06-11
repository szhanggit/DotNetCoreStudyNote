using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
using Xunit;

namespace Unit.Test
{
    public class TestProductMemoService : CommonHelper
    {
        [Fact]
        public async Task Test_UpdateProductMemo_Case0()
        {
            ProductMemoService _memoService;
            IDbConnection _dbConnection = InitDbConnection<ProductMemoService>(out _memoService);


        }

        [Fact]
        public async Task Test_GetProductMemo_Case0()
        {
            ProductMemoService _memoService;
            IDbConnection _dbConnection = InitDbConnection<ProductMemoService>(out _memoService);

        }

        [Fact]
        public async Task Test_ProductMemoIsTheSame_Case0()
        {
            ProductMemoService _memoService;
            IDbConnection _dbConnection = InitDbConnection<ProductMemoService>(out _memoService);


        }

        [Fact]
        public async Task Test_SendProductUpdateMemoMessage_Case0()
        {
            int tenantId = 1;
            string queueName = "tx2connectorqueue-gl-dev";
            var _updateProductMemoV1 = new UpdateProductMemoV1()
            {
                ProductId = 1,
                TenantId = 1,
                TX2UserName = "stzhang",
                SalesNote = "asdf",
                OperationNote = "asdf",
                CustomerServiceNote = "asdfa"
            };
            ProductMemoService _productMemoService;
            ITX2ServiceBusSender _sender = InitServiceBus<ProductMemoService>(out _productMemoService);
            bool flag = await _productMemoService.SendProductUpdateMemoMessage(tenantId, queueName, _updateProductMemoV1);
            Assert.True(flag);
        }
    }
}
