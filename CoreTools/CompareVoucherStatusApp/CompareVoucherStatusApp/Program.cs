using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace CompareVoucherStatusApp
{
    class Program
    {
		private static IContainer Container { get; set; }
		static void Main(string[] args)
        {
			var startup = new Startup();
			var serviceProvider = startup.BuildContainer();
			var app = serviceProvider.GetService<IApp>();
			app.Run();
			Console.ReadKey();
			startup.DisposeServices();
		}
	}
}
