using System;

namespace MVCProxy.Core.Classes
{
    public class ProxyHttpRoute
    {
        public string Name { get; set; }
        public string RouteTemplate { get; set; }
        public Object Defaults { get; set; }
        public Object Constraints { get; set; }
    }
}
