using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;

namespace SimpleWPFDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void App_OnStartup(object sender, StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(@"C:\Temp\TournamentTracker\WPFDemoLog.txt")
                .CreateLogger();

            try
            {
                var host = CreateHostBuilder(new string[0]).Build();

                host.Services.GetService<MainWindow>().ShowDialog();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "There was a major problem that crashed the application.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<MainWindow>();
                    services.AddTransient<IDoWork, DoWork>();
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
    }
}
