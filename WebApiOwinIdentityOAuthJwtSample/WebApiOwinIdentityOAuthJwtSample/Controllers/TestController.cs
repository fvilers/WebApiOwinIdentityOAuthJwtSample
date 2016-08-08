using System.Web.Http;

namespace WebApiOwinIdentityOAuthJwtSample.Controllers
{
    [RoutePrefix("test")]
    public class TestController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
