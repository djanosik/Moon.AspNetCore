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
                .Configure<RazorViewEngineOptions>(o =>
                {
                    o.ViewLocationFormats.Clear();
                    o.ViewLocationFormats.Add("/Pages/{1}/{0}.cshtml");
                    o.ViewLocationFormats.Add("/Pages/Shared/{0}.cshtml");
                })
                .AddMvc()
                .AddHttpErrors();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}