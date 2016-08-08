using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiOwinIdentityOAuthJwtSample.Models;

namespace WebApiOwinIdentityOAuthJwtSample.Controllers
{
    [RoutePrefix("identity")]
    public class IdentityController : ApiController
    {
        private const string GetUserRouteName = "GetUser";
        private UserManager<IdentityUser> UserManager => Request.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();

        [Authorize]
        [HttpGet]
        [Route("{userId}", Name = GetUserRouteName)]
        public async Task<IHttpActionResult> Get(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Register(Registration model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (await UserManager.FindByNameAsync(model.UserName) != null)
            {
                return Conflict();
            }

            var user = new IdentityUser(model.UserName);
            var result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return CreatedAtRoute(GetUserRouteName, new { userId = user.Id }, user);
        }
    }
}
