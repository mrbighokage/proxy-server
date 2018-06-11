using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;

namespace MVCProxy.Core.Application
{
    public static class HttpConfigurationExtensions
    {
        public static void EnableProxy(this HttpConfiguration httpConfig)
        {
            httpConfig.Routes.MapHttpRoute(
                name: "Proxy",
                routeTemplate: "{*path}",
                handler: HttpClientFactory.CreatePipeline(
                    innerHandler: new HttpClientHandler(),
                    handlers: new DelegatingHandler[]
                    {
                        new ProxyHandler()
                    }
                ),
                defaults: new { path = RouteParameter.Optional },
                constraints: null
            );
        }

        public class ProxyHandler : DelegatingHandler
        {
            private static HttpClient client = new HttpClient();

            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                var forwardUri = new UriBuilder(request.RequestUri.AbsoluteUri);
                forwardUri.Host = "localhost";
                forwardUri.Port = 62904;
                request.RequestUri = forwardUri.Uri;

                if (request.Method == HttpMethod.Get)
                {
                    request.Content = null;
                }

                request.Headers.Add("X-Forwarded-Host", request.Headers.Host);
                request.Headers.Host = "localhost:62904";
                var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                return response;
            }
        }
    }
}
