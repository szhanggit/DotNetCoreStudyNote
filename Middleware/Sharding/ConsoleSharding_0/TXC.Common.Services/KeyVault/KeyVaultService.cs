using System;
using Microsoft.Extensions.Configuration;

namespace TXC.Common.Services.KeyVault
{
    public class KeyVaultService : IKeyVaultServices
    {
        private readonly IConfiguration _config;
        public KeyVaultService(IConfiguration config)
        {
            _config = config;
        }

        public string GetKeyVaultSecret(string key)
        {
            try
            {
                return _config.GetValue<string>(key);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
