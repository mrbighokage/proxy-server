using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using MVCProxy.Core.Classes;

namespace MVCProxy.Core.Extenssions
{
    public static class HttpConfigurationExtensions
    {
        public static void EnableProxy(this HttpConfiguration httpConfig, string host, int port,
            IEnumerable<ProxyHttpRoute> proxyRoutes)
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
