using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Moon.AspNetCore.Authentication.Basic;

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthenticationBuilderExtensions
    {
        /// <summary>
        /// Adds a Basic authentication to your web application pipeline.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder)
            => builder.AddBasic(BasicAuthenticationDefaults.AuthenticationScheme);

        /// <summary>
        /// Adds a Basic authentication to your web application pipeline.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="authenticationScheme">The name of authentication scheme.</param>
        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string authenticationScheme)
            => builder.AddBasic(authenticationScheme, null);

        /// <summary>
        /// Adds a Basic authentication to your web application pipeline.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="configureOptions">An action to configure Basic authentication.</param>
        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, Action<BasicAuthenticationOptions> configureOptions)
            => builder.AddBasic(BasicAuthenticationDefaults.AuthenticationScheme, configureOptions);

        /// <summary>
        /// Adds a Basic authentication to your web application pipeline.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="authenticationScheme">The name of authentication scheme.</param>
        /// <param name="configureOptions">An action to configure Basic authentication.</param>
        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string authenticationScheme, Action<BasicAuthenticationOptions> configureOptions)
            => builder.AddBasic(authenticationScheme, null, configureOptions);

        /// <summary>
        /// Adds a Basic authentication to your web application pipeline.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="authenticationScheme">The name of authentication scheme.</param>
        /// <param name="displayName">The display name of the authentication scheme.</param>
        /// <param name="configureOptions">An action to configure Basic authentication.</param>
        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<BasicAuthenticationOptions> configureOptions)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<BasicAuthenticationOptions>, PostConfigureBasicAuthenticationOptions>());
            return builder.AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}