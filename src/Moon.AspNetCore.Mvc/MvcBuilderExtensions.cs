using Moon.AspNetCore.Mvc;
using Moon.AspNetCore.Mvc.Filters;

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcBuilderExtensions
    {
        /// <summary>
        /// Adds an action filter required to safely throw <see cref="HttpException" /> in action methods.
        /// </summary>
        /// <param name="builder">The ASP.NET MVC builder.</param>
        public static IMvcBuilder AddHttpErrors(this IMvcBuilder builder)
            => builder.AddMvcOptions(o => o.Filters.Add(new HttpExceptionActionFilter()));
    }
}