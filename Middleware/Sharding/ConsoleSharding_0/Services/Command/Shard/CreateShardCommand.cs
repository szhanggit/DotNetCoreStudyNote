using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.Services.Wrappers;

namespace Services.Command.Shard
{
    public class CreateShardCommand : IRequestWrapper<bool>
    {
        public string DbName { get; set; }
        public int Key { get; set; }
    }
}
