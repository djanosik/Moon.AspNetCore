using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Moon.AspNet.Mvc.Sample
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app
                .UseIISPlatformHandler()
                .UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IRazorViewEngine, PagesViewEngine>()
                .AddMvc()
                .AddHttpErrors();
        }
    }
}