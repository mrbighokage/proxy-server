using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using MVCProxy.Core.Application;
using MVCProxy.Core.View;
using MVCProxy.Tests.API;

[assembly: PreApplicationStartMethod(typeof(ProxyConfig), "Register")]

namespace MVCProxy.Tests.API
{
    public class ProxyConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableProxy("duediligerdevbackend-env.wsupjtzrpx.eu-central-1.elasticbeanstalk.com", 80,
                    new List<ProxyHttpRoute>
                    {
                        new ProxyHttpRoute()
                        {
                            Name = "route 1",
                            RouteTemplate = "api/{string1}/{string2}",
                            Defaults = new { string2 = RouteParameter.Optional },
                            Constraints = new { string1 = @"\w+", string2 = @"\w+" }
                        }
                    }
                );
        }
    }
}
