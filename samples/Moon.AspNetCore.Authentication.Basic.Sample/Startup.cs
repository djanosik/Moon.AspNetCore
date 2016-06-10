using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Moon.AspNetCore.Mvc;

namespace Moon.AspNetCore.Authentication.Basic.Sample
{
    public class Startup
    {
        readonly string password = "password";

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAuthorization()
                .AddSingleton<IRazorViewEngine, PagesViewEngine>()
                .AddMvc();
        }

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
    }
}