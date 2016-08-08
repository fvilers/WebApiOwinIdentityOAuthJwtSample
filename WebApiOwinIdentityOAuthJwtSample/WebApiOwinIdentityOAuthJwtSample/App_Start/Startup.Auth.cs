using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;
using System.Text;
using WebApiOwinIdentityOAuthJwtSample.Security;

namespace WebApiOwinIdentityOAuthJwtSample
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            var issuer = ConfigurationManager.AppSettings["jwt:issuer"];
            var audience = ConfigurationManager.AppSettings["jwt:audience"];
            var key = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["jwt:secret"]);
            var expiration = TimeSpan.FromHours(Int32.Parse(ConfigurationManager.AppSettings["jwt:expiration"]));

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = expiration,
                Provider = new IdentityAuthorizationServerProvider(),
                AccessTokenFormat = new JwtDataFormat(issuer, audience, key, expiration)
            });

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AllowedAudiences = new[] { audience },
                IssuerSecurityTokenProviders = new[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, key)
                }
            });
        }
    }
}