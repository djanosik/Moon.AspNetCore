using Moon.AspNet.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="IMvcBuilder" /> extension methods.
    /// </summary>
    public static class MvcBuilderExtensions
    {
        /// <summary>
        /// Adds an action filter required to safely throw <see cref="HttpException" /> in action methods.
        /// </summary>
        /// <param name="services">The ASP.NET MVC builder.</param>
        public static IMvcBuilder AddHttpErrors(this IMvcBuilder builder)
            => builder.AddMvcOptions(o =>
            {
                o.Filters.Add(new HttpExceptionActionFilter());
            });
    }
}