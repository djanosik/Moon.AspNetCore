using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;

namespace Moon.AspNet.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().Configure<MvcOptions>(o =>
            {
                o.ViewEngines.Clear();
                o.ViewEngines.Add(typeof(PagesViewEngine));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsForDevelopment())
            {
                app.UseOneHostName("domain.com");
            }

            app.UseMvc();
        }
    }
}