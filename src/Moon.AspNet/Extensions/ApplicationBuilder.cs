using System.Text.RegularExpressions;
using Moon;
using Moon.AspNet;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// <see cref="IApplicationBuilder" /> extension methods.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Redirects request to preferred host name when not-matching host name is detected.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="hostName">The preferred host name (eg. "onedomain.com").</param>
        public static IApplicationBuilder UseOneHostName(this IApplicationBuilder app, string hostName)
            => app.UseOneHostName(hostName, GetPattern(hostName));

        /// <summary>
        /// Redirects request to preferred host name when not-matching host name is detected.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="hostName">The preferred host name (eg. "onedomain.com").</param>
        /// <param name="pattern">The pattern used to match the preferred host name (eg. "^onedomain\.com$").</param>
        public static IApplicationBuilder UseOneHostName(this IApplicationBuilder app, string hostName, string pattern)
        {
            Requires.NotNullOrWhiteSpace(hostName, nameof(hostName));
            Requires.NotNull(pattern, nameof(pattern));

            var options = new OneHostNameOptions
            {
                HostName = hostName,
                Pattern = new Regex(pattern, RegexOptions.Singleline)
            };

            return app.UseMiddleware<OneHostNameMiddleware>(options);
        }

        static string GetPattern(string hostName)
            => $"^{Regex.Escape(hostName)}$";
    }
}