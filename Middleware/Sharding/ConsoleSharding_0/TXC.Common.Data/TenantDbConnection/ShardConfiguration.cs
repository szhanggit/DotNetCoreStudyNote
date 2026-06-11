using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXC.Common.Data.TenantDbConnection
{
    public class ShardConfiguration
    {
        public string GlobalShardMapConnectionString { get; set; }
        public string ShardMapName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
