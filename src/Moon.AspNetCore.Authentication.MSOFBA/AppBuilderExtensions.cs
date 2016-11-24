using Microsoft.Extensions.Options;
using Moon;
using Moon.AspNetCore.Authentication.MSOFBA;

namespace Microsoft.AspNetCore.Builder
{
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Adds an MS-OFBA authentication middleware to your web application pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static IApplicationBuilder UseMSOFBAuthentication(this IApplicationBuilder app)
            => app.UseMiddleware<MSOFBAuthenticationMiddleware>();

        /// <summary>
        /// Adds an MS-OFBA authentication middleware to your web application pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="options">The middleware options configuration.</param>
        public static IApplicationBuilder UseMSOFBAuthentication(this IApplicationBuilder app, MSOFBAuthenticationOptions options)
        {
            Requires.NotNull(options, nameof(options));

            return app.UseMiddleware<MSOFBAuthenticationMiddleware>(Options.Create(options));
        }
    }
}