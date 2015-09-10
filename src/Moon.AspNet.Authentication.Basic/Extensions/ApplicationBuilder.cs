using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.OptionsModel;
using Moon.AspNet.Authentication.Basic;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// <see cref="IApplicationBuilder" /> extension methods.
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Adds a Basic authentication middleware to your web application pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configureOptions">The middleware options configuration.</param>
        public static IApplicationBuilder UseBasicAuthentication(this IApplicationBuilder app, Action<BasicAuthenticationOptions> configureOptions = null)
            => app.UseMiddleware<BasicAuthenticationMiddleware>(new ConfigureOptions<BasicAuthenticationOptions>(configureOptions ?? (o => { })));

        /// <summary>
        /// Adds a cookie-based authentication middleware to your web application pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="options">&gt;The middleware options configuration.</param>
        public static IApplicationBuilder UseBasicAuthentication(this IApplicationBuilder app, IOptions<BasicAuthenticationOptions> options)
            => app.UseMiddleware<BasicAuthenticationMiddleware>(options, new ConfigureOptions<BasicAuthenticationOptions>(o => { }));
    }
}