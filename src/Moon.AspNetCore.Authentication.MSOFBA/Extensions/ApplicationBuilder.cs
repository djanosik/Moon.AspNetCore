using System;
using Moon;
using Moon.AspNetCore.Authentication.MSOFBA;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// <see cref="IApplicationBuilder" /> extension methods.
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Adds an MS-OFBA authentication middleware to your web application pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static IApplicationBuilder UseMSOFBAuthentication(this IApplicationBuilder app)
            => app.UseMSOFBAuthentication(new MSOFBAuthenticationOptions());

        /// <summary>
        /// Adds an MS-OFBA authentication middleware to your web application pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configureOptions">The middleware options configuration.</param>
        public static IApplicationBuilder UseMSOFBAuthentication(this IApplicationBuilder app, Action<MSOFBAuthenticationOptions> configureOptions)
        {
            var options = new MSOFBAuthenticationOptions();
            configureOptions?.Invoke(options);

            return app.UseMSOFBAuthentication(options);
        }

        /// <summary>
        /// Adds an MS-OFBA authentication middleware to your web application pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="options">The middleware options configuration.</param>
        public static IApplicationBuilder UseMSOFBAuthentication(this IApplicationBuilder app, MSOFBAuthenticationOptions options)
        {
            Requires.NotNull(options, nameof(options));

            return app.UseMiddleware<MSOFBAuthenticationMiddleware>(options);
        }
    }
}