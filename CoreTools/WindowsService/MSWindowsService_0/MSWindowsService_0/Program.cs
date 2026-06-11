using System;
using System.ServiceProcess;
using System.Threading;

namespace MSWindowsService_0
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggingService = new LoggingService();
            if (false)   //Some check to see if we are in debug mode (Either #IF Debug etc or an app setting)
            {
                loggingService.OnStartPublic(new string[0]);
                while (true)
                {
                    //Just spin wait here. 
                    Thread.Sleep(1000);
                }
                //Call stop here etc. 
            }
            else
            { 
                ServiceBase.Run(new LoggingService()); 
            }
            
        }
    }
}
