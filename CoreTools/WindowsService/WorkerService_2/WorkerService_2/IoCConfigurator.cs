using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerService.Lib;

namespace WorkerService_2
{
    public class IoCConfigurator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CalService>().As<ICalService>();
            //builder.RegisterType<Cal2Service>().As<ICal2Service>();
            
        }
    }
}
