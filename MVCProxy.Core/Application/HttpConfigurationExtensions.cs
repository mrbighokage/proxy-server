using MVCProxy.Core.View;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace MVCProxy.Core.Application
{
    public static class HttpConfigurationExtensions
    {
        public static void EnableProxy(this HttpConfiguration httpConfig, string host, int port,
            List<ProxyHttpRoute> proxyRoutes)
        {
            foreach (var route in proxyRoutes)
            {
                httpConfig.Routes.MapHttpRoute(
                    name: route.Name,
                    routeTemplate: route.RouteTemplate,
                    handler: HttpClientFactory.CreatePipeline(
                        innerHandler: new HttpClientHandler(),
                        handlers: new DelegatingHandler[]
                        {
                            new ProxyHandler(host, port)
                        }
                    ),
                    defaults: route.Defaults,
                    constraints: route.Constraints
                );
            }
        }
    }
}
