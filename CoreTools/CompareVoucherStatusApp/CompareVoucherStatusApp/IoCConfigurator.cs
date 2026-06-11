using Autofac;
using CompareVoucherStatusApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareVoucherStatusApp
{
    public class IoCConfigurator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SerilogService>().As<ISerilogService>();
            builder.RegisterType<VoucherRepository>().As<IVoucherRepository>();
            builder.RegisterType<App>().As<IApp>();
            builder.RegisterType<SettingService>().As<ISetting>();
            builder.RegisterType<ExtendVoucherRepository>().As<IExtendVoucherRepository>();
            builder.RegisterType<DataTableToDataListHelper>().As<IDataTableToDataListHelper>();
            builder.RegisterType<SqlHelper>().As<ISqlHelper>();            
        }
    }
}
