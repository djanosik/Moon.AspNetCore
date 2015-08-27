using System;

namespace Microsoft.AspNet.Hosting
{
    /// <summary>
    /// <see cref="IHostingEvironment" /> extension methods.
    /// </summary>
    public static class HostingEnvironmentExtensions
    {
        /// <summary>
        /// Returns whether this is a development environment.
        /// </summary>
        /// <param name="env">The environment.</param>
        public static bool IsForDevelopment(this IHostingEnvironment env)
            => env.IsFor("Development");

        /// <summary>
        /// Returns whether this is a specified environment.
        /// </summary>
        /// <param name="env">The hosting environment.</param>
        /// <param name="environment">The environment name (case-insensitive).</param>
        public static bool IsFor(this IHostingEnvironment env, string environment)
            => string.Equals(env.EnvironmentName, environment, StringComparison.OrdinalIgnoreCase);
    }
}