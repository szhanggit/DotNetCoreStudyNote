using Microsoft.AspNetCore.Http;

namespace SportsStore.Infrastructure
{
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest request)// => request.QueryString.HasValue? $"{request.Path}{request.QueryString}" : request.Path.ToString();
        {
            if (request.QueryString.HasValue)
            {
                string restr = $"{request.Path}{request.QueryString}";
                return restr;
            }
            else
            { 
                string path = request.Path.ToString();
                return path;
            }
        }
    }
}
