using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Moon.AspNetCore.Authentication.Basic.Sample
{
    public class Startup
    {
        private const string password = "password";

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<RazorViewEngineOptions>(o =>
                {
                    o.ViewLocationFormats.Clear();
                    o.ViewLocationFormats.Add("/Pages/{1}/{0}.cshtml");
                    o.ViewLocationFormats.Add("/Pages/Shared/{0}.cshtml");
                })
                .AddAuthorization()
                .AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            // WARNING! Never ever use the Basic authentication with non-SSL connection.

            app.UseBasicAuthentication(new BasicAuthenticationOptions {
                Realm = $"Password: {password}",
                Events = new BasicAuthenticationEvents {
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
                }
            });

            app.UseMvc();
        }
    }
}