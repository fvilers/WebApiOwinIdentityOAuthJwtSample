using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(WebApiOwinIdentityOAuthJwtSample.Startup))]

namespace WebApiOwinIdentityOAuthJwtSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            
            ConfigureAuth(app);
            ConfigureWebApi(app);
        }
    }
}
