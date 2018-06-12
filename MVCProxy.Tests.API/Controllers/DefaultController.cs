using System.Web.Http;

namespace MVCProxy.Tests.API.Controllers
{
    [Route("api/Default/{Action}")]
    public class DefaultController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Test1()
        {
            return Ok();
        }
    }
}
