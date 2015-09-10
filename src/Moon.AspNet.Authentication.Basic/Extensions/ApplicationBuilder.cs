using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.OptionsModel;

namespace Moon.AspNet.Authentication.Basic
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

        ///// <summary>
        ///// Adds MS-OFBA authentication middleware to the OWIN pipeline.
        ///// </summary>
        //public static IAppBuilder UseMicrosoftOfficeFormsBasedAuthentication(this IAppBuilder app, MSOFBAuthenticationOptions options)
        //    => app.Use<MSOFBAuthenticationMiddleware>(options);

        ///// <summary>
        ///// Adds MS-OFBA authentication middleware to the OWIN pipeline.
        ///// </summary>
        //public static IAppBuilder UseMicrosoftOfficeFormsBasedAuthentication(this IAppBuilder app, string loginPath, string loginSuccessPath = "/")
        //    => app.UseMicrosoftOfficeFormsBasedAuthentication(new MSOFBAuthenticationOptions
        //    {
        //        LoginPath = new PathString(loginPath),
        //        LoginSuccessPath = new PathString(loginSuccessPath)
        //    });
    }
}