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
using TXC.Proto.Product;
using Xunit;

namespace Unit.Test
{
    public class TestExternalPropertyService : CommonHelper
    {
        [Fact]
        public async Task Test_InsertTimeRestriction_Case0()
        {
            ExternalPropertyService _externalPropertyService;
            IDbConnection _dbConnection = InitDbConnection<ExternalPropertyService>(out _externalPropertyService);
            List<ProductExternalPropertySet> _list = new List<ProductExternalPropertySet> {
                new ProductExternalPropertySet{ PropertyName = "A", PropertyValue = "aa", Description = "" },
                new ProductExternalPropertySet{ PropertyName = "B", PropertyValue = "bb", Description = "" },
                new ProductExternalPropertySet{ PropertyName = "C", PropertyValue = "cc", Description = "" },                
            };

            int result = await _externalPropertyService.InsertExternalProperty(_list, 1, "sz", _dbConnection);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Test_GetExternalPropertyList_Case0()
        {
            ExternalPropertyService _externalPropertyService;
            IDbConnection _dbConnection = InitDbConnection<ExternalPropertyService>(out _externalPropertyService);
            int ProductId = 1;
            IEnumerable<ExternalPropertyItem> _restrictionSet = await _externalPropertyService.GetExternalPropertyList(ProductId, _dbConnection);
        }

        [Fact]
        public async Task Test_DeleteExternalProperty_Case0()
        {
            ExternalPropertyService _externalPropertyService;
            IDbConnection _dbConnection = InitDbConnection<ExternalPropertyService>(out _externalPropertyService);
            int ProductId = 1;
            string UserName = "stzhang";
            int result = await _externalPropertyService.DeleteExternalProperty(ProductId, UserName, _dbConnection);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Test_SendProductExternalPropertyMessage_Case0()
        {
            int tenantId = 1;
            string queueName = "tx2connectorqueue-gl-dev";
            var _createProductExternalPropertyV1 = new CreateProductExternalPropertyV1()
            {
                ProductId = 1,
                TenantId = 1,
                TX2UserName = "stzhang",
                ExternalProperties = new List<ExternalProperty> { 
                    new ExternalProperty{ PropertyName = "AA1", PropertyValue = "AA1", Description = "AA1" },
                    new ExternalProperty{ PropertyName = "BB1", PropertyValue = "BB1", Description = "BB1" },
                    new ExternalProperty{ PropertyName = "CC1", PropertyValue = "CC1", Description = "CC1" },
                }
            };
            ExternalPropertyService _externalPropertyService;
            ITX2ServiceBusSender _sender = InitServiceBus<ExternalPropertyService>(out _externalPropertyService);
            bool flag = await _externalPropertyService.SendProductCreateExternalPropertyMessage(tenantId, queueName, _createProductExternalPropertyV1);
            Assert.True(flag);
        }

        [Fact]
        public async Task Test_SendDeleteProductExternalPropertyMessage_Case0()
        {
            int tenantId = 1;
            string queueName = "tx2connectorqueue-gl-dev";
            var _createProductExternalPropertyV1 = new CreateProductExternalPropertyV1()
            {
                ProductId = 1,
                TenantId = 1,
                TX2UserName = "stzhang"
            };
            ExternalPropertyService _externalPropertyService;
            ITX2ServiceBusSender _sender = InitServiceBus<ExternalPropertyService>(out _externalPropertyService);
            bool flag = await _externalPropertyService.SendProductDeleteExternalPropertyMessage(tenantId, queueName, _createProductExternalPropertyV1);
            Assert.True(flag);
        }
    }
}
