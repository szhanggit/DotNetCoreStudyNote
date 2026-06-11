using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareVoucherStatusApp
{
    public class SettingService : ISetting
    {
        private IConfigurationBuilder builder = null;
        private IConfigurationRoot config = null;
        private string _connectionString = string.Empty;
        private int _runningStep = 0;
        public SettingService()
        {
            builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
            config = builder.Build();
        }

        public string MoveConnection
        {
            get {
                _connectionString = config["ConnectionStrings:MoveConnection"];
                return _connectionString; 
            }
        }

        public string LocalConnection
        {
            get
            {
                _connectionString = config["ConnectionStrings:LocalConnection"];
                return _connectionString;
            }
        }

        public int RunningStep
        {
            get 
            {
                string Step = config["RunningStep"];
                if (int.TryParse(Step, out _runningStep))
                { 
                    return _runningStep; 
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
