using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace TestSerilogConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
			SetupStaticLogger();

			try
			{
				RunApp();
				Log.Warning("Testing");
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "An unhandled exception occurred.");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		private static void SetupStaticLogger()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();

			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.CreateLogger();
		}

		private static void RunApp()
		{
			// Do not pass any logger in via Dependency Injection, as the class will simply reference the static logger.
			var classThatLogs = new ClassThatLogs();
			classThatLogs.WriteLogs();
		}
	}
}
