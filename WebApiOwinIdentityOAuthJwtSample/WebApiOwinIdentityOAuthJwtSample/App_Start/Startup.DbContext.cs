using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;
using System;

namespace WebApiOwinIdentityOAuthJwtSample
{
    public partial class Startup
    {
        public void ConfigureDbContext(IAppBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.CreatePerOwinContext(() => new IdentityDbContext());
            app.CreatePerOwinContext<UserManager<IdentityUser>>(CreateManager);
        }

        private static UserManager<IdentityUser> CreateManager(IdentityFactoryOptions<UserManager<IdentityUser>> options, IOwinContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var dbContext = context.Get<IdentityDbContext>();

            if (dbContext == null)
            {
                throw new InvalidOperationException("An IdentityDbContext is not registered within the OWIN context.");
            }

            var store = new UserStore<IdentityUser>(dbContext);
            var manager = new UserManager<IdentityUser>(store);

            return manager;
        }
    }
}