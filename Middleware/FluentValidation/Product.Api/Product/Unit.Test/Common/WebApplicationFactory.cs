using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace Unit.Test.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
    }
}
