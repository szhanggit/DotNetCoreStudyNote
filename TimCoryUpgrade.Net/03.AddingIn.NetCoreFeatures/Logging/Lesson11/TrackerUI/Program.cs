using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;

// Serilog example - https://github.com/serilog/serilog-extensions-logging/blob/dev/samples/Sample/Program.cs

namespace TrackerUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(@"C:\Temp\TournamentTracker\WinformLog.txt")
                .CreateLogger();

            try
            {
                var host = CreateHostBuilder(args).Build();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Initialize the database connections
                TrackerLibrary.GlobalConfig.InitializeConnections(DatabaseType.TextFile);
                Application.Run(host.Services.GetService<TournamentDashboardForm>());
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
                    services.AddTransient<TournamentDashboardForm>();
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
