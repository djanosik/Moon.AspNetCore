using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Moon.AspNetCore.Authentication.Basic.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAuthorization()
                .AddMvc();

            // WARNING! Never ever use the Basic authentication with non-SSL connection.

            services
                .AddAuthentication("Basic")
                .AddBasic(o =>
                {
                    o.Realm = "Password: password";

                    o.Events = new BasicAuthenticationEvents {
                        OnSignIn = OnSignIn
                    };
                });
        }

        public void Configure(IApplicationBuilder app)
            => app
                .UseAuthentication()
                .UseMvc();

        private Task OnSignIn(BasicSignInContext context)
        {
            if (context.Password == "password")
            {
                var claims = new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, context.UserName) };
                var identity = new ClaimsIdentity(claims, context.Scheme.Name);
                context.Principal = new ClaimsPrincipal(identity);
            }

            return Task.CompletedTask;
        }
    }
}