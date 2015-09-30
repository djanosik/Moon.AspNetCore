using Microsoft.AspNet.Mvc;
using Moon.AspNet.Mvc;

namespace Microsoft.Framework.DependencyInjection
{
    /// <summary>
    /// <see cref="IMvcBuilder" /> extension methods.
    /// </summary>
    public static class MvcBuilderExtensions
    {
        /// <summary>
        /// Adds an action filter required to safely throw <see cref="HttpException" /> s in action methods.
        /// </summary>
        /// <param name="services">The ASP.NET MVC builder.</param>
        public static IMvcBuilder AddHttpErrors(this IMvcBuilder builder)
        {
            var services = builder.Services;

            services.Configure<MvcOptions>(o =>
            {
                o.Filters.Add(new HttpExceptionActionFilter());
            });

            return builder;
        }
    }
}