using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Product.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    Console.WriteLine("Starting..." + context.HostingEnvironment.EnvironmentName);

                    // add secrets from CSI environment variables
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        // for gRPC
                        options.ListenAnyIP(9018, o => o.Protocols =
                            HttpProtocols.Http2);

                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
