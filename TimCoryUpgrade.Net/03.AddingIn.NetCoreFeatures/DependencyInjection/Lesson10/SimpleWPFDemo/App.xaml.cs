using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleWPFDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void App_OnStartup(object sender, StartupEventArgs e)
        {
            var host = CreateHostBuilder(new string[0]).Build();

            host.Services.GetService<MainWindow>().ShowDialog();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<MainWindow>();
                    services.AddTransient<IDoWork, DoWork>();
                });
        }
    }
}
