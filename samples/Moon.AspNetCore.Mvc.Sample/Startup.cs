using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Moon.AspNetCore.Mvc.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
            => services
                .AddMvc()
                .AddHttpErrors();

        public void Configure(IApplicationBuilder app)
            => app
                .UseMvc();
    }
}