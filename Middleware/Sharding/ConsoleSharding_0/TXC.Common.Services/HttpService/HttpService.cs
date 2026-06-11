using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TXC.Common.Services.HttpService
{
    public class HttpService : IHttpService
    {
        private readonly System.Net.Http.IHttpClientFactory _httpClientFactory;
        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TData> PostAsync<TData>(Uri uri, HttpContent content, Dictionary<string, string> headers, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();


                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrWhiteSpace(item.Key)
                            && !string.IsNullOrEmpty(item.Value) && !string.IsNullOrWhiteSpace(item.Value))
                        {
                            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                }

                var httpResponseMessage = await httpClient.PostAsync(uri, content, cancellationToken);

                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<TData>(responseContent);
                return data;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<TData> PutAsync<TData>(Uri uri, HttpContent content, Dictionary<string, string> headers, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();


                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrWhiteSpace(item.Key)
                            && !string.IsNullOrEmpty(item.Value) && !string.IsNullOrWhiteSpace(item.Value))
                        {
                            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                }

                var httpResponseMessage = await httpClient.PutAsync(uri, content, cancellationToken);

                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<TData>(responseContent);
                return data;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TData> GetAsync<TData>(Uri uri, Dictionary<string, string> headers, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();


                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrWhiteSpace(item.Key)
                            && !string.IsNullOrEmpty(item.Value) && !string.IsNullOrWhiteSpace(item.Value))
                        {
                            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                }

                var httpResponseMessage = await httpClient.GetAsync(uri, cancellationToken);

                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<TData>(responseContent);
                return data;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TData> DeleteAsync<TData>(Uri uri, Dictionary<string, string> headers, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();


                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrWhiteSpace(item.Key)
                            && !string.IsNullOrEmpty(item.Value) && !string.IsNullOrWhiteSpace(item.Value))
                        {
                            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                }

                var httpResponseMessage = await httpClient.DeleteAsync(uri, cancellationToken);

                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<TData>(responseContent);
                return data;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TData> SendAsync<TData>(Uri uri, Dictionary<string, string> headers, HttpMethod httpMethod, HttpContent httpContent, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, uri);

                foreach (var header in headers)
                {
                    if (!string.IsNullOrEmpty(header.Key) && !string.IsNullOrWhiteSpace(header.Key)
                        && !string.IsNullOrEmpty(header.Value) && !string.IsNullOrWhiteSpace(header.Value))
                    {
                        httpRequestMessage.Headers.Add(header.Key, header.Value);
                    }
                }

                if (httpContent != null)
                {
                    httpRequestMessage.Content = httpContent;
                }
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, cancellationToken);

                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<TData>(responseContent);
                return data;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent content, Dictionary<string, string> headers, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();


                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrWhiteSpace(item.Key)
                            && !string.IsNullOrEmpty(item.Value) && !string.IsNullOrWhiteSpace(item.Value))
                        {
                            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                }

                var httpResponseMessage = await httpClient.PostAsync(uri, content, cancellationToken);

                return httpResponseMessage;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<HttpResponseMessage> PutAsync(Uri uri, HttpContent content, Dictionary<string, string> headers, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();


                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrWhiteSpace(item.Key)
                            && !string.IsNullOrEmpty(item.Value) && !string.IsNullOrWhiteSpace(item.Value))
                        {
                            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                }

                var httpResponseMessage = await httpClient.PutAsync(uri, content, cancellationToken);

                return httpResponseMessage;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<HttpResponseMessage> GetAsync(Uri uri, Dictionary<string, string> headers, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();


                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrWhiteSpace(item.Key)
                            && !string.IsNullOrEmpty(item.Value) && !string.IsNullOrWhiteSpace(item.Value))
                        {
                            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                }

                var httpResponseMessage = await httpClient.GetAsync(uri, cancellationToken);

                return httpResponseMessage;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(Uri uri, Dictionary<string, string> headers, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();


                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrWhiteSpace(item.Key)
                            && !string.IsNullOrEmpty(item.Value) && !string.IsNullOrWhiteSpace(item.Value))
                        {
                            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                }

                var httpResponseMessage = await httpClient.DeleteAsync(uri, cancellationToken);

                return httpResponseMessage;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<HttpResponseMessage> SendAsync(Uri uri, Dictionary<string, string> headers, HttpMethod httpMethod, HttpContent httpContent, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, uri);

                foreach (var header in headers)
                {
                    if (!string.IsNullOrEmpty(header.Key) && !string.IsNullOrWhiteSpace(header.Key)
                        && !string.IsNullOrEmpty(header.Value) && !string.IsNullOrWhiteSpace(header.Value))
                    {
                        httpRequestMessage.Headers.Add(header.Key, header.Value);
                    }
                }

                if (httpContent != null)
                {
                    httpRequestMessage.Content = httpContent;
                }
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, cancellationToken);

                return httpResponseMessage;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
