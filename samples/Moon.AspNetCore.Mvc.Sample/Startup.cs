using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Moon.AspNetCore.Mvc.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IRazorViewEngine, PagesViewEngine>()
                .AddMvc()
                .AddHttpErrors();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}