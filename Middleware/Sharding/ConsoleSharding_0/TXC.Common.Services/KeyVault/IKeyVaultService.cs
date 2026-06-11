using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXC.Common.Services.KeyVault
{
    public interface IKeyVaultServices
    {
        public string GetKeyVaultSecret(string key);
    }
}
