using AutoMapper;
using Infrastructure.Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Api;
using Services.Core;
using Services.Interface;
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

namespace Unit.Test
{
    public class CommonHelper
    {
        protected IDbConnection InitDbConnection<T>(out T _service) where T : new()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            string _connectionString = config["ConnectionString"];

            Startup startup = new Startup(config);
            ServiceCollection sc = new ServiceCollection();
            startup.ConfigureServices(sc);
            IServiceProvider serviceProvider = sc.BuildServiceProvider();

            ITxcCacheReadFactory _txcCacheReadFactory = new TxcCacheReadFactoryGrpc(serviceProvider);
            IDapperOperation _dapperOperation = new DapperOperation();
            ITenantConfigHelper _tenantConfigHelper = new TenantConfigHelper(_txcCacheReadFactory);
            ITX2ServiceBusSender _txcServiceBusSender = new TX2ServiceBusSender(config);
            T objItem = new T();
            object[] args = new object[] { _dapperOperation, _txcServiceBusSender };
            objItem = (T)Activator.CreateInstance(typeof(T), args);
            IDbConnection _dbConnection = new SqlConnection(_connectionString);
            _service = objItem;
            return _dbConnection;
        }

        protected ITX2ServiceBusSender InitServiceBus<T>(out T _service) where T : new()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            Startup startup = new Startup(config);
            ServiceCollection sc = new ServiceCollection();
            startup.ConfigureServices(sc);
            IServiceProvider serviceProvider = sc.BuildServiceProvider();

            ITxcCacheReadFactory _txcCacheReadFactory = new TxcCacheReadFactoryGrpc(serviceProvider);
            IDapperOperation _dapperOperation = new DapperOperation();
            ITenantConfigHelper _tenantConfigHelper = new TenantConfigHelper(_txcCacheReadFactory);
            ITX2ServiceBusSender _txcServiceBusSender = new TX2ServiceBusSender(config);
            T objItem = new T();
            object[] args = new object[] { _dapperOperation, _txcServiceBusSender };
            objItem = (T)Activator.CreateInstance(typeof(T), args);
            _service = objItem;
            return _txcServiceBusSender;
        }

        protected IObjectConvertingService GetObjectConvertingService()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            IMapper _mapper = new Mapper(mapperConfig);
            IObjectConvertingService ObjectConvertingService = new ObjectConvertingService(_mapper);
            return ObjectConvertingService;
        }
    }
}
