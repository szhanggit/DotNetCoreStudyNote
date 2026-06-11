using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerService.Lib;

namespace WorkerService_2
{
    public class IoCConfigurator2 : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QuickScanService>().As<IQuickScanService>();
            builder.RegisterType<SlowScanService>().As<ISlowScanService>(); 
        }
    }
}
