using System;
using Topshelf;

namespace TopshelfService_0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
                {
                    x.Service<LoggingService>();
                    x.EnableServiceRecovery(r => r.RestartService(TimeSpan.FromSeconds(10)));
                    x.SetServiceName("TestTopshelfService");
                    x.StartAutomatically();
                }
            );
        }
    }
}
