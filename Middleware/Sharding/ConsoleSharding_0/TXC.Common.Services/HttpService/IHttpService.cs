using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TXC.Common.Services.HttpService
{
    public interface IHttpService
    {
        public Task<TData> PostAsync<TData>(Uri uri, HttpContent content, Dictionary<string, string> headers, CancellationToken cancellationToken);
        public Task<TData> PutAsync<TData>(Uri uri, HttpContent content, Dictionary<string, string> headers, CancellationToken cancellationToken);
        public Task<TData> GetAsync<TData>(Uri uri, Dictionary<string, string> headers, CancellationToken cancellationToken);
        public Task<TData> DeleteAsync<TData>(Uri uri, Dictionary<string, string> headers, CancellationToken cancellationToken);
        public Task<TData> SendAsync<TData>(Uri uri, Dictionary<string, string> headers, HttpMethod httpMethod, HttpContent httpContent, CancellationToken cancellationToken);
        public Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent content, Dictionary<string, string> headers, CancellationToken cancellationToken);
        public Task<HttpResponseMessage> PutAsync(Uri uri, HttpContent content, Dictionary<string, string> headers, CancellationToken cancellationToken);
        public Task<HttpResponseMessage> GetAsync(Uri uri, Dictionary<string, string> headers, CancellationToken cancellationToken);
        public Task<HttpResponseMessage> DeleteAsync(Uri uri, Dictionary<string, string> headers, CancellationToken cancellationToken);
        public Task<HttpResponseMessage> SendAsync(Uri uri, Dictionary<string, string> headers, HttpMethod httpMethod, HttpContent httpContent, CancellationToken cancellationToken);

    }
}
