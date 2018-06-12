using System;

namespace MVCProxy.Core.View
{
    public class ProxyHttpRoute
    {
        public ProxyHttpRoute()
        {
            Defaults = new { };
            Constraints = new { };
        }

        public string Name { get; set; }
        public string RouteTemplate { get; set; }
        public Object Defaults { get; set; }
        public Object Constraints { get; set; }
    }
}
