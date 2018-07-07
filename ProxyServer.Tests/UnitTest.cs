using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProxyServer.Tests.Classes;
using ProxyServer.Tests.Mocks;

namespace ProxyServer.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public async Task CheckRequestWithProxy()
        {
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", TestRequestData.BearerToken);

            var forwardUri = new UriBuilder(TestRequestData.ForwardUrl);
            request.RequestUri = forwardUri.Uri;

            var handler = new ProxyHandlerTest(TestRequestData.Host, TestRequestData.Port);
            Assert.IsTrue(await handler.CompileTestAsync(request));
        }
    }
}
