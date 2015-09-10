using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Framework.DependencyInjection;
using Moon.AspNet.Mvc;

namespace Moon.AspNet.Authentication.Basic.Sample
{
    public class Startup
    {
        readonly string password = "password";

        public void Configure(IApplicationBuilder app)
        {
            // WARNING! Never ever use the Basic authentication with non-SSL connection.

            app.UseBasicAuthentication(o =>
            {
                o.Realm = $"Password: {password}";

                o.Events = new BasicAuthenticationEvents
                {
                    OnSignIn = c =>
                    {
                        if (c.Password == password)
                        {
                            var claims = new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, c.UserName) };
                            var identity = new ClaimsIdentity(claims, c.Options.AuthenticationScheme);
                            c.Principal = new ClaimsPrincipal(identity);
                        }

                        return Task.FromResult(true);
                    }
                };
            });

            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IRazorViewEngine, PagesViewEngine>();
            services.AddAuthorization();
        }
    }
}