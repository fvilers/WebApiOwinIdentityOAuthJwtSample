using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiOwinIdentityOAuthJwtSample.Security
{
    internal class IdentityAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() =>
            {
                context.Validated();
            });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(context.UserName);
            var authenticated = await userManager.CheckPasswordAsync(user, context.Password);

            if (!authenticated)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", user.Id));
            identity.AddClaim(new Claim("unique_name", user.UserName));

            // TODO: Add custom claims to identity

            context.Validated(identity);
        }
    }
}