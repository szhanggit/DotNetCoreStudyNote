using Azure.Messaging.ServiceBus;
using Domain.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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
using TXC.Common.Domain;
using TXC.Common.MessageContract;
using TXC.Common.MessageContract.Product;
using TXC.Common.Security.Cryptography;
using TXC.Proto.Product;
using Xunit;

namespace Unit.Test
{
    public class TestUpdateProductConditionService : CommonHelper
    {
        ProductUpdatingConditionRequest request = null;
        Mock<CheckProductDel> _checkProductDelMock = null;
        Mock<GetConfigDel> _getConfigDelMock = null;
        Mock<IDbConnection> _connectionMock = null;
        Mock<GetDBConnectionDel> _getDBConnectionDelMock = null;
        Response<IDbConnection> conn = null;   
        Mock<UpdateProductConditionDel> _updateProductConditionDelMock = null;
        Mock<SendProductConditionMessageDel> _sendProductConditionMessageDelMock = null;
        Mock<CompareConditionIsTheSameDel> _compareConditionIsTheSameDelMock = null;
        Mock<GetProductConditionDel> _getProductConditionDelMock = null;
        ProductConditionResponse _productConditionResponse = null;
        Mock<CheckReverseLimitNameDel> _checkReverseLimitNameDelMock = null;
        Mock<ConvertProductConditionResponseToProductConditionDel> _convertProductConditionResponseToProductConditionDelMock = null;
        Mock<ProductCondition> _productConditionMock0 = null;
        Mock<ConvertProductUpdatingConditionRequestToProductConditionDel> _convertProductUpdatingConditionRequestToProductConditionDelMock = null;
        Mock<ProductCondition> _productConditionMock1 = null;
        Mock<ConvertProductUpdatingConditionRequestToUpdateProductConditionV1Del> _convertProductUpdatingConditionRequestToUpdateProductConditionV1DelMock = null;
        Mock<UpdateProductConditionV1> _updateProductConditionV1Mock = null;

        private void InitMock()
        {
            request = new ProductUpdatingConditionRequest
            {
                TenantId = 1,
                TenantName = "Thailand",
                TX2UserName = "stzhang",
                ProductId = 1,
                MinRedeemQuantity = 2,
                MaxIssuingQuantity = null,
                ValidFrom = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(1900, 01, 01), DateTimeKind.Utc)),
                ValidTo = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(3999, 01, 01), DateTimeKind.Utc)),
                ReverseLimitId = 2,
                PreAuthorizationExpiryInterval = 3,
                PreAuthorizationExpiryUnit = 2
            };
            _checkProductDelMock = new Mock<CheckProductDel>();
            _checkProductDelMock.Setup(p => p(It.IsAny<int>(), It.IsAny<IDbConnection>())).ReturnsAsync(new Tuple<int, int>(1, 1));
            _getConfigDelMock = new Mock<GetConfigDel>();
            _getConfigDelMock.Setup(p => p(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new TXC.Common.Domain.TenantConfig
            {
                TenantId = 1,
                ConfigType = "asdf",
                ConfigName = "asdfasdf",
                Value = "",
                Version = "1",
                Comment = ""
            });

            _connectionMock = new Mock<IDbConnection>();
            _getDBConnectionDelMock = new Mock<GetDBConnectionDel>();
            conn = new Response<IDbConnection>();
            conn.Data = new SqlConnection("Data Source=esg-txcloud-new-asse-sqlsrv-d.privatelink.database.windows.net;Initial Catalog=txc_dev_tenant_th;User ID=txc-dev-admin;Password=fUjRkDeX8LDe4pC3;MultipleActiveResultSets=true");
            conn.Success = true;
            _getDBConnectionDelMock.Setup(p => p(It.IsAny<int>())).ReturnsAsync(conn);
            _updateProductConditionDelMock = new Mock<UpdateProductConditionDel>();
            _updateProductConditionDelMock.Setup(p => p(It.IsAny<ProductUpdatingConditionRequest>(), It.IsAny<IDbConnection>())).ReturnsAsync(1);
            _sendProductConditionMessageDelMock = new Mock<SendProductConditionMessageDel>();
            _sendProductConditionMessageDelMock.Setup(p => p(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<UpdateProductConditionV1>())).ReturnsAsync(true);
            _compareConditionIsTheSameDelMock = new Mock<CompareConditionIsTheSameDel>();
            _compareConditionIsTheSameDelMock.Setup(p => p(It.IsAny<ProductCondition>(), It.IsAny<ProductCondition>())).Returns(false);
            _getProductConditionDelMock = new Mock<GetProductConditionDel>();
            _productConditionResponse = new ProductConditionResponse
            {
                MinRedeemQuantity = 2,
                MaxIssuingQuantity = null,
                ValidFrom = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(1900, 01, 01), DateTimeKind.Utc)),
                ValidTo = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(3999, 01, 01), DateTimeKind.Utc)),
                ReverseLimitId = 2,
                PreAuthorizationExpiryInterval = 3,
                PreAuthorizationExpiryUnit = 2
            };
            _getProductConditionDelMock.Setup(p => p(It.IsAny<int>(), It.IsAny<IDbConnection>())).ReturnsAsync(_productConditionResponse);
            _checkReverseLimitNameDelMock = new Mock<CheckReverseLimitNameDel>();
            _checkReverseLimitNameDelMock.Setup(p => p(It.IsAny<int>(), It.IsAny<IDbConnection>())).ReturnsAsync(true);
            _convertProductConditionResponseToProductConditionDelMock = new Mock<ConvertProductConditionResponseToProductConditionDel>();
            _productConditionMock0 = new Mock<ProductCondition>();
            _convertProductConditionResponseToProductConditionDelMock.Setup(p => p(It.IsAny<ProductConditionResponse>())).Returns(_productConditionMock0.Object);
            _convertProductUpdatingConditionRequestToProductConditionDelMock = new Mock<ConvertProductUpdatingConditionRequestToProductConditionDel>();
            _productConditionMock1 = new Mock<ProductCondition>();
            _convertProductUpdatingConditionRequestToProductConditionDelMock.Setup(p => p(It.IsAny<ProductUpdatingConditionRequest>())).Returns(_productConditionMock1.Object);
            _convertProductUpdatingConditionRequestToUpdateProductConditionV1DelMock = new Mock<ConvertProductUpdatingConditionRequestToUpdateProductConditionV1Del>();
            _updateProductConditionV1Mock = new Mock<UpdateProductConditionV1>();
            _convertProductUpdatingConditionRequestToUpdateProductConditionV1DelMock.Setup(p => p(It.IsAny<ProductUpdatingConditionRequest>())).Returns(_updateProductConditionV1Mock.Object);
        }

        [Fact]
        public async Task Test_UpdateProductCondition_Case0()
        {
            ProductUpdatingConditionRequest request = new ProductUpdatingConditionRequest { 
                TenantId = 1,
                TenantName = "Thailand",
                TX2UserName = "stzhang",
                ProductId = 1,
                MinRedeemQuantity = 2, 
                MaxIssuingQuantity = null,
                ValidFrom = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(1900,01,01), DateTimeKind.Utc)),
                ValidTo = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(3999, 01, 01), DateTimeKind.Utc)),
                ReverseLimitId = 2,
                PreAuthorizationExpiryInterval = 3,
                PreAuthorizationExpiryUnit = 2
            };
            CheckProductDel _checkProductDel = async (ProductId, _dbConnection) => { return new Tuple<int, int>(1, 1); };
            GetConfigDel _getConfigDel = async (ConfigName, TenantId) => { return new TXC.Common.Domain.TenantConfig { 
                TenantId = 1, ConfigType = "asdf", ConfigName = "asdfasdf", Value = "", Version = "1", Comment = ""
            }; };
            GetDBConnectionDel _getDBConnectionDel = async (TenantId) => { 
                Response<IDbConnection> conn = new Response<IDbConnection>();
                conn.Data = new SqlConnection("Data Source=esg-txcloud-new-asse-sqlsrv-d.privatelink.database.windows.net;Initial Catalog=txc_dev_tenant_th;User ID=txc-dev-admin;Password=fUjRkDeX8LDe4pC3;MultipleActiveResultSets=true");
                conn.Success = true;
                return conn;
            };
            UpdateProductConditionDel _updateProductConditionDel = async (request, _dbConnection) => 1;
            SendProductConditionMessageDel _sendProductConditionMessageDel = async (TenantId, queneNameConfig, message) => true;
            CompareConditionIsTheSameDel _compareConditionIsTheSameDel = (item1, item2) => false;
            GetProductConditionDel _getProductConditionDel = async (ProductId, _dbConnection) => new ProductConditionResponse {
                MinRedeemQuantity = 2,
                MaxIssuingQuantity = null,
                ValidFrom = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(1900, 01, 01), DateTimeKind.Utc)),
                ValidTo = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(3999, 01, 01), DateTimeKind.Utc)),
                ReverseLimitId = 2,
                PreAuthorizationExpiryInterval = 3,
                PreAuthorizationExpiryUnit = 2
            };
            CheckReverseLimitNameDel _checkReverseLimitName = async (ReverseLimitNameId, _dbConnection) => { return true; };
            ConvertProductConditionResponseToProductConditionDel _convertProductConditionResponseToProductConditionDel = (input) => new ProductCondition { };
            ConvertProductUpdatingConditionRequestToProductConditionDel _convertProductUpdatingConditionRequestToProductConditionDel = (input) => new ProductCondition { };
            ConvertProductUpdatingConditionRequestToUpdateProductConditionV1Del _convertProductUpdatingConditionRequestToUpdateProductConditionV1Del = (input) => new UpdateProductConditionV1 { };


            IUpdateProductConditionService _productConditionService = new UpdateProductConditionService();
            var result = await _productConditionService.UpdateProductCondition(
                request
                , _checkProductDel
                , _getConfigDel
                , _getDBConnectionDel
                , _updateProductConditionDel
                , _sendProductConditionMessageDel
                , _compareConditionIsTheSameDel
                , _getProductConditionDel
                , _checkReverseLimitName
                , _convertProductConditionResponseToProductConditionDel
                , _convertProductUpdatingConditionRequestToProductConditionDel
                , _convertProductUpdatingConditionRequestToUpdateProductConditionV1Del);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Test_UpdateProductCondition_HappyPath()
        {
            InitMock();
            IUpdateProductConditionService _productConditionService = new UpdateProductConditionService();
            var result = await _productConditionService.UpdateProductCondition(
                request
                , _checkProductDelMock.Object
                , _getConfigDelMock.Object
                , _getDBConnectionDelMock.Object
                , _updateProductConditionDelMock.Object
                , _sendProductConditionMessageDelMock.Object
                , _compareConditionIsTheSameDelMock.Object
                , _getProductConditionDelMock.Object
                , _checkReverseLimitNameDelMock.Object
                , _convertProductConditionResponseToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToUpdateProductConditionV1DelMock.Object);


            Assert.True(result.Success);
            Assert.Equal("Success", result.Message);
            Assert.Equal(1, result.Data);
        }

        [Fact]
        public async Task Test_UpdateProductCondition_NoTenantName()
        {
            InitMock();
            request.TenantName = null;
            IUpdateProductConditionService _productConditionService = new UpdateProductConditionService();
            var result = await _productConditionService.UpdateProductCondition(
                request
                , _checkProductDelMock.Object
                , _getConfigDelMock.Object
                , _getDBConnectionDelMock.Object
                , _updateProductConditionDelMock.Object
                , _sendProductConditionMessageDelMock.Object
                , _compareConditionIsTheSameDelMock.Object
                , _getProductConditionDelMock.Object
                , _checkReverseLimitNameDelMock.Object
                , _convertProductConditionResponseToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToUpdateProductConditionV1DelMock.Object);


            Assert.False(result.Success);
            Assert.Equal("TenantName header required", result.Message);
            Assert.Equal(0, result.Data);
        }

        [Fact]
        public async Task Test_UpdateProductCondition_ProductNotExist()
        {
            InitMock();
            _checkProductDelMock.Setup(p => p(It.IsAny<int>(), It.IsAny<IDbConnection>())).ReturnsAsync(new Tuple<int, int>(-1, 1));
            IUpdateProductConditionService _productConditionService = new UpdateProductConditionService();
            var result = await _productConditionService.UpdateProductCondition(
                request
                , _checkProductDelMock.Object
                , _getConfigDelMock.Object
                , _getDBConnectionDelMock.Object
                , _updateProductConditionDelMock.Object
                , _sendProductConditionMessageDelMock.Object
                , _compareConditionIsTheSameDelMock.Object
                , _getProductConditionDelMock.Object
                , _checkReverseLimitNameDelMock.Object
                , _convertProductConditionResponseToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToUpdateProductConditionV1DelMock.Object);


            Assert.False(result.Success);
            Assert.Equal("Product not found", result.Message);
            Assert.Equal(0, result.Data);
        }

        [Fact]
        public async Task Test_UpdateProductCondition_InvalidRequest()
        {
            InitMock();
            request.ProductId = -1;
            IUpdateProductConditionService _productConditionService = new UpdateProductConditionService();
            var result = await _productConditionService.UpdateProductCondition(
                request
                , _checkProductDelMock.Object
                , _getConfigDelMock.Object
                , _getDBConnectionDelMock.Object
                , _updateProductConditionDelMock.Object
                , _sendProductConditionMessageDelMock.Object
                , _compareConditionIsTheSameDelMock.Object
                , _getProductConditionDelMock.Object
                , _checkReverseLimitNameDelMock.Object
                , _convertProductConditionResponseToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToUpdateProductConditionV1DelMock.Object);


            Assert.False(result.Success);
            Assert.Equal("Invalid request", result.Message);
            Assert.Equal(0, result.Data);
        }

        [Fact]
        public async Task Test_UpdateProductCondition_ErrorInTenantDB()
        {
            InitMock();
            conn.Success = false;
            IUpdateProductConditionService _productConditionService = new UpdateProductConditionService();
            var result = await _productConditionService.UpdateProductCondition(
                request
                , _checkProductDelMock.Object
                , _getConfigDelMock.Object
                , _getDBConnectionDelMock.Object
                , _updateProductConditionDelMock.Object
                , _sendProductConditionMessageDelMock.Object
                , _compareConditionIsTheSameDelMock.Object
                , _getProductConditionDelMock.Object
                , _checkReverseLimitNameDelMock.Object
                , _convertProductConditionResponseToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToUpdateProductConditionV1DelMock.Object);


            Assert.False(result.Success);
            Assert.Equal("Error in Tenant DB", result.Message);
            Assert.Equal(0, result.Data);
        }

        [Fact]
        public async Task Test_UpdateProductCondition_InvalidReverseLimitId()
        {
            InitMock();
            _checkReverseLimitNameDelMock.Setup(p => p(It.IsAny<int>(), It.IsAny<IDbConnection>())).ReturnsAsync(false);
            IUpdateProductConditionService _productConditionService = new UpdateProductConditionService();
            var result = await _productConditionService.UpdateProductCondition(
                request
                , _checkProductDelMock.Object
                , _getConfigDelMock.Object
                , _getDBConnectionDelMock.Object
                , _updateProductConditionDelMock.Object
                , _sendProductConditionMessageDelMock.Object
                , _compareConditionIsTheSameDelMock.Object
                , _getProductConditionDelMock.Object
                , _checkReverseLimitNameDelMock.Object
                , _convertProductConditionResponseToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToUpdateProductConditionV1DelMock.Object);
            
            Assert.False(result.Success);
            Assert.Equal("Invalid Reverse Limit Id", result.Message);
            Assert.Equal(0, result.Data);
        }

        [Fact]
        public async Task Test_UpdateProductCondition_NoChange()
        {
            InitMock();
            _compareConditionIsTheSameDelMock.Setup(p => p(It.IsAny<ProductCondition>(), It.IsAny<ProductCondition>())).Returns(true);
            IUpdateProductConditionService _productConditionService = new UpdateProductConditionService();
            var result = await _productConditionService.UpdateProductCondition(
                request
                , _checkProductDelMock.Object
                , _getConfigDelMock.Object
                , _getDBConnectionDelMock.Object
                , _updateProductConditionDelMock.Object
                , _sendProductConditionMessageDelMock.Object
                , _compareConditionIsTheSameDelMock.Object
                , _getProductConditionDelMock.Object
                , _checkReverseLimitNameDelMock.Object
                , _convertProductConditionResponseToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToUpdateProductConditionV1DelMock.Object);


            Assert.False(result.Success);
            Assert.Equal("No Change", result.Message);
            Assert.Equal(0, result.Data);
        }

        [Fact]
        public async Task Test_UpdateProductCondition_FailToUpdate()
        {
            InitMock();
            _updateProductConditionDelMock.Setup(p => p(It.IsAny<ProductUpdatingConditionRequest>(), It.IsAny<IDbConnection>())).ReturnsAsync(0);
            IUpdateProductConditionService _productConditionService = new UpdateProductConditionService();
            var result = await _productConditionService.UpdateProductCondition(
                request
                , _checkProductDelMock.Object
                , _getConfigDelMock.Object
                , _getDBConnectionDelMock.Object
                , _updateProductConditionDelMock.Object
                , _sendProductConditionMessageDelMock.Object
                , _compareConditionIsTheSameDelMock.Object
                , _getProductConditionDelMock.Object
                , _checkReverseLimitNameDelMock.Object
                , _convertProductConditionResponseToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToUpdateProductConditionV1DelMock.Object);


            Assert.False(result.Success);
            Assert.Equal("Fail", result.Message);
            Assert.Equal(1, result.Data);
        }

        [Fact]
        public async Task Test_UpdateProductCondition_FailSendToServiceBus()
        {
            InitMock();
            _sendProductConditionMessageDelMock.Setup(p => p(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<UpdateProductConditionV1>())).ReturnsAsync(false);
            IUpdateProductConditionService _productConditionService = new UpdateProductConditionService();
            var result = await _productConditionService.UpdateProductCondition(
                request
                , _checkProductDelMock.Object
                , _getConfigDelMock.Object
                , _getDBConnectionDelMock.Object
                , _updateProductConditionDelMock.Object
                , _sendProductConditionMessageDelMock.Object
                , _compareConditionIsTheSameDelMock.Object
                , _getProductConditionDelMock.Object
                , _checkReverseLimitNameDelMock.Object
                , _convertProductConditionResponseToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToProductConditionDelMock.Object
                , _convertProductUpdatingConditionRequestToUpdateProductConditionV1DelMock.Object);


            Assert.False(result.Success);
            Assert.Equal("Fail to send to service bus.", result.Message);
            Assert.Equal(0, result.Data);
        }
    }
}