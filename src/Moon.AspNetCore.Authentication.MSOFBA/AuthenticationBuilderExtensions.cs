using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Moon.AspNetCore.Authentication.MSOFBA;

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthenticationBuilderExtensions
    {
        /// <summary>
        /// Adds a MS-OFBA authentication to your web application pipeline.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        public static AuthenticationBuilder AddMSOFBA(this AuthenticationBuilder builder)
            => builder.AddMSOFBA(CookieAuthenticationDefaults.AuthenticationScheme);

        /// <summary>
        /// Adds a MS-OFBA authentication to your web application pipeline.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="authenticationScheme">The name of authentication scheme.</param>
        public static AuthenticationBuilder AddMSOFBA(this AuthenticationBuilder builder, string authenticationScheme)
            => builder.AddMSOFBA(authenticationScheme, null);

        /// <summary>
        /// Adds a MS-OFBA authentication to your web application pipeline.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="configureOptions">An action to configure MS-OFBA authentication.</param>
        public static AuthenticationBuilder AddMSOFBA(this AuthenticationBuilder builder, Action<MSOFBAuthenticationOptions> configureOptions)
            => builder.AddMSOFBA(CookieAuthenticationDefaults.AuthenticationScheme, configureOptions);

        /// <summary>
        /// Adds a MS-OFBA authentication to your web application pipeline.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="authenticationScheme">The name of authentication scheme.</param>
        /// <param name="configureOptions">An action to configure MS-OFBA authentication.</param>
        public static AuthenticationBuilder AddMSOFBA(this AuthenticationBuilder builder, string authenticationScheme, Action<MSOFBAuthenticationOptions> configureOptions)
            => builder.AddMSOFBA(authenticationScheme, null, configureOptions);

        /// <summary>
        /// Adds a MS-OFBA authentication to your web application pipeline.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="authenticationScheme">The name of authentication scheme.</param>
        /// <param name="displayName">The display name of the authentication scheme.</param>
        /// <param name="configureOptions">An action to configure MS-OFBA authentication.</param>
        public static AuthenticationBuilder AddMSOFBA(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<MSOFBAuthenticationOptions> configureOptions)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<MSOFBAuthenticationOptions>, PostConfigureMSOFBAuthenticationOptions>());
            return builder.AddScheme<MSOFBAuthenticationOptions, MSOFBAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}