using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Framework.DependencyInjection;
using Moon.AspNet.Mvc;

namespace Moon.AspNet.Sample
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseOneHostName("domain.com");
            }

            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IRazorViewEngine, PagesViewEngine>();
        }
    }
}