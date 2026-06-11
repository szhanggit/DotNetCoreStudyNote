using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacSerilogIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace CompareVoucherStatusApp
{
    public class Startup
    {
        public const string AspnetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public const string Production = "Production";

        public IConfigurationRoot Configuration { get; }

        public ContainerBuilder ContainerBuilder { get; set; }

        protected static IServiceProvider ServiceProvider { get; set; }

        public Startup()
        {
            var envName = Environment.GetEnvironmentVariable(AspnetCoreEnvironment) ?? Production;

            #if DEBUG
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            #else
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .AddEnvironmentVariables();
            #endif

            Configuration = builder.Build();

            var services = new ServiceCollection();
            ContainerBuilder = ConfigureServices(services);
            /*****************************************************/            
            var appContainer = ContainerBuilder.Build();
            ServiceProvider = new AutofacServiceProvider(appContainer);
        }



        public IServiceProvider BuildContainer()
        {
            //return ContainerBuilder.Build().Resolve<IServiceProvider>();
            return ServiceProvider;
        }

        private ContainerBuilder ConfigureServices(IServiceCollection serviceCollection)
        {
            CreateLogger(Configuration);

            serviceCollection.AddAutofac();
            serviceCollection.AddOptions();

            var builder = new ContainerBuilder();
            builder.RegisterModule(new IoCConfigurator());
            builder.Populate(serviceCollection);
            builder.RegisterLogger();

            return builder;
        }

        public static void CreateLogger(IConfigurationRoot configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public void DisposeServices()
        {
            Log.CloseAndFlush();
            if (ServiceProvider == null)
            {
                return;
            }
            if (ServiceProvider is IDisposable)
            {
                ((IDisposable)ServiceProvider).Dispose();
            }
        }
    }
}
