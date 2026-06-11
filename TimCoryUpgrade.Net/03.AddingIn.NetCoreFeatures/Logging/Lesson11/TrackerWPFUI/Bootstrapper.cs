using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrackerWPFUI.ViewModels;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

// Simple Container Doc - https://caliburnmicro.com/documentation/simple-container

namespace TrackerWPFUI
{
    public class Bootstrapper : BootstrapperBase
    {
        IHost host;

        public Bootstrapper()
        {
            Initialize();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(@"C:\Temp\TournamentTracker\CaliburnMicroWPFLog.txt")
                .CreateLogger();

            host = CreateHostBuilder(new string[0]).Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<ShellViewModel>();
                    services.AddSingleton<IWindowManager, WindowManager>();
                    services.AddSingleton<IEventAggregator, EventAggregator>();
                    services.AddSingleton<ILoggerFactory>(x =>
                    {
                        var providerCollection = x.GetService<LoggerProviderCollection>();
                        var factory = new SerilogLoggerFactory(null, true, providerCollection);

                        foreach (var provider in x.GetServices<ILoggerProvider>())
                        {
                            factory.AddProvider(provider);
                        }

                        return factory;
                    });
                });
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return host.Services.GetService(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return host.Services.GetServices(service);
        }

        protected override void BuildUp(object instance)
        {
            
        }
    }
}
