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
    public class TestProductRestrictionService : CommonHelper
    {
        [Fact]
        public async Task Test_InsertTimeRestriction_Case0()
        {
            ProductRestrictionService _productRestrictionService;
            IDbConnection _dbConnection = InitDbConnection<ProductRestrictionService>(out _productRestrictionService);
            List<ProductRedeemTimeRestrictionSet> _list = new List<ProductRedeemTimeRestrictionSet> { 
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 0, StartTime = new TimeSpan(1,0,0), EndTime = new TimeSpan(8,0,0) },
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 1, StartTime = new TimeSpan(1,0,0), EndTime = new TimeSpan(8,0,0) },
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 2, StartTime = new TimeSpan(1,0,0), EndTime = new TimeSpan(8,0,0) },
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 3, StartTime = new TimeSpan(1,0,0), EndTime = new TimeSpan(8,0,0) },
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 4, StartTime = new TimeSpan(1,0,0), EndTime = new TimeSpan(8,0,0) },
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 5, StartTime = new TimeSpan(1,0,0), EndTime = new TimeSpan(8,0,0) },
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 6, StartTime = new TimeSpan(1,0,0), EndTime = new TimeSpan(8,0,0) },
            };
            
            int result = await _productRestrictionService.InsertTimeRestriction(_list, 1, "sz", _dbConnection);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Test_GetTimeRestrictionList_Case0()
        {
            ProductRestrictionService _productRestrictionService;
            IDbConnection _dbConnection = InitDbConnection<ProductRestrictionService>(out _productRestrictionService);
            int ProductId = 1;
            IEnumerable<ProductRedeemTimeRestrictionSet> _restrictionSet = await _productRestrictionService.GetTimeRestrictionList(ProductId, _dbConnection);
            StringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _helper = new RestrictionHelper(_stringHelper);
            List<string> result = await _helper.SerializeRestrictionTimeList(_restrictionSet);
        }

        [Fact]
        public async Task Test_DeleteTimeRestriction_Case0()
        {
            ProductRestrictionService _productRestrictionService;
            IDbConnection _dbConnection = InitDbConnection<ProductRestrictionService>(out _productRestrictionService);
            int ProductId = 1;
            string UserName = "stzhang";
            int result = await _productRestrictionService.DeleteTimeRestriction(ProductId, UserName, _dbConnection);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Test_InsertDateRestriction_Case0()
        {
            ProductRestrictionService _productRestrictionService;
            IDbConnection _dbConnection = InitDbConnection<ProductRestrictionService>(out _productRestrictionService);
            List<DateTime> _list = new List<DateTime> {
                new DateTime(2022,01,01),
                new DateTime(2022,02,01),
                new DateTime(2022,03,01),
            };

            int result = await _productRestrictionService.InsertDateRestriction(_list, 1, "sz", _dbConnection);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Test_GetDateRestrictionList_Case0()
        {
            ProductRestrictionService _productRestrictionService;
            IDbConnection _dbConnection = InitDbConnection<ProductRestrictionService>(out _productRestrictionService);
            int ProductId = 1;
            IEnumerable<ProductRedeemDateRestrictionSet> _restrictionSet = await _productRestrictionService.GetDateRestrictionList(ProductId, _dbConnection);
        }

        [Fact]
        public async Task Test_DeleteDateRestriction_Case0()
        {
            ProductRestrictionService _productRestrictionService;
            IDbConnection _dbConnection = InitDbConnection<ProductRestrictionService>(out _productRestrictionService);
            int ProductId = 1;
            string UserName = "stzhang";
            int result = await _productRestrictionService.DeleteDateRestriction(ProductId, UserName, _dbConnection);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Test_SendProductCreateRestrictionMessage_Case0()
        {
            int tenantId = 1;
            string queueName = "tx2connectorqueue-gl-dev";
            var _createProductRestrictionV1 = new CreateProductRestrictionV1()
            {
                ProductId = 1,
                TenantId = 1,
                TX2UserName = "stzhang",
                BlackDateList = new List<DateTime> { 
                    new DateTime(2022,2,1), new DateTime(2022,4,2), new DateTime(2022,6,3) 
                },
                TimeRestrictionList = new List<TimeRestriction> { 
                    //new TimeRestriction{ DayOfWeek = DayOfWeek.Sunday, StartTime = new TimeSpan(0,0,0), EndTime = new TimeSpan(22,00,00) },
                    //new TimeRestriction{ DayOfWeek = DayOfWeek.Monday, StartTime = new TimeSpan(0,0,0), EndTime = new TimeSpan(22,00,00) },
                    //new TimeRestriction{ DayOfWeek = DayOfWeek.Tuesday, StartTime = new TimeSpan(0,0,0), EndTime = new TimeSpan(22,00,00) },
                    //new TimeRestriction{ DayOfWeek = DayOfWeek.Wednesday, StartTime = new TimeSpan(0,0,0), EndTime = new TimeSpan(22,00,00) },
                    //new TimeRestriction{ DayOfWeek = DayOfWeek.Thursday, StartTime = new TimeSpan(0,0,0), EndTime = new TimeSpan(22,00,59) },
                    //new TimeRestriction{ DayOfWeek = DayOfWeek.Friday, StartTime = new TimeSpan(0,0,0), EndTime = new TimeSpan(22,00,00) },
                    //new TimeRestriction{ DayOfWeek = DayOfWeek.Saturday, StartTime = new TimeSpan(0,0,0), EndTime = new TimeSpan(22,00,00) },
                }
            };
            ProductRestrictionService _productRestrictionService;
            ITX2ServiceBusSender _sender = InitServiceBus<ProductRestrictionService>(out _productRestrictionService);
            bool flag = await _productRestrictionService.SendProductCreateRestrictionMessage(tenantId, queueName, _createProductRestrictionV1);
            Assert.True(flag);
        }

        [Fact]
        public async Task Test_SendProductDeleteRestrictionMessage_Case0()
        {
            int tenantId = 1;
            string queueName = "tx2connectorqueue-gl-dev";
            var _createProductRestrictionV1 = new CreateProductRestrictionV1()
            {
                ProductId = 1,
                TenantId = 1,
                TX2UserName = "stzhang"               
            };
            ProductRestrictionService _productRestrictionService;
            ITX2ServiceBusSender _sender = InitServiceBus<ProductRestrictionService>(out _productRestrictionService);
            bool flag = await _productRestrictionService.SendProductDeleteRestrictionMessage(tenantId, queueName, _createProductRestrictionV1);
            Assert.True(flag);
        }
    }
}
