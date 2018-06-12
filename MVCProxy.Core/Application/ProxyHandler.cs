using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MVCProxy.Core.Application
{
    internal class ProxyHandler : DelegatingHandler
    {
        private readonly HttpClient _client;
        private readonly string _host;
        private readonly int _port;

        public ProxyHandler(string host, int port)
        {
            _host = host;
            _port = port;
            _client = new HttpClient();
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var forwardUri = new UriBuilder(request.RequestUri.AbsoluteUri)
            {
                Host = _host,
                Port = _port
            };

            // replace the port to the backend target port
            request.RequestUri = forwardUri.Uri;

            // replace the Host header when making the request to the 
            // backend node
            request.Headers.Host = $"{_host}:{_port}";
            var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            return response;
        }
    }
}
