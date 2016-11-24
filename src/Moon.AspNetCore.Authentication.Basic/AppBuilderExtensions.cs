using Microsoft.Extensions.Options;
using Moon;
using Moon.AspNetCore.Authentication.Basic;

namespace Microsoft.AspNetCore.Builder
{
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Adds a Basic authentication middleware to your web application pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static IApplicationBuilder UseBasicAuthentication(this IApplicationBuilder app)
            => app.UseMiddleware<BasicAuthenticationMiddleware>();

        /// <summary>
        /// Adds a cookie-based authentication middleware to your web application pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="options">The middleware options configuration.</param>
        public static IApplicationBuilder UseBasicAuthentication(this IApplicationBuilder app, BasicAuthenticationOptions options)
        {
            Requires.NotNull(options, nameof(options));

            return app.UseMiddleware<BasicAuthenticationMiddleware>(Options.Create(options));
        }
    }
}