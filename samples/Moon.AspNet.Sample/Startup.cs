using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using Moon.AspNet.Mvc;

namespace Moon.AspNet.Sample
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsForDevelopment())
            {
                app.UseOneHostName("domain.com");
            }

            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().ConfigureMvcViews(o =>
            {
                o.ViewEngines.Clear();
                o.ViewEngines.Add(typeof(PagesViewEngine));
            });
        }
    }
}