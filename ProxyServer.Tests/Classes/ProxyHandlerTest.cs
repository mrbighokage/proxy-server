using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MVCProxy.Core.Classes;

namespace ProxyServer.Tests.Classes
{
    internal class ProxyHandlerTest : ProxyHandler
    {
        public ProxyHandlerTest(string host, int port) : base(host, port) { }

        public async Task<bool> CompileTestAsync(HttpRequestMessage request)
        {
            var cts = new CancellationTokenSource();
            var responce = await SendAsync(request, cts.Token);

            return responce.StatusCode == HttpStatusCode.OK;
        }
    }
}
