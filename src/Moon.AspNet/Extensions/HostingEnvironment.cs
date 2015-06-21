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
            => string.Equals(env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase);
    }
}