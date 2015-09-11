using System;
using Moon.AspNet.Authentication.MSOFBA;

namespace Microsoft.AspNet.Builder
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
        /// <param name="configureOptions">The middleware options configuration.</param>
        public static IApplicationBuilder UseMSOFBAuthentication(this IApplicationBuilder app, Action<MSOFBAuthenticationOptions> configureOptions = null)
        {
            var options = new MSOFBAuthenticationOptions();

            if (configureOptions != null)
            {
                configureOptions(options);
            }

            return app.UseMiddleware<MSOFBAuthenticationMiddleware>(options);
        }
    }
}