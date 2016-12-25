using Microsoft.AspNetCore.Http;
using Moon.AspNetCore.Rewrite;

// ReSharper disable once CheckNamespace

namespace Microsoft.AspNetCore.Rewrite
{
    public static class RewriteOptionsExtensions
    {
        /// <summary>
        /// Redirect a request to the specified host / domain if the current host is different,
        /// with returning a 301 status code for permanently redirected.
        /// </summary>
        /// <param name="options">The rewrite options.</param>
        /// <param name="host">The canonical host (eg. "domain.com").</param>
        public static RewriteOptions AddRedirectToCanonicalHostPermanent(this RewriteOptions options, string host)
            => options.AddRedirectToCanonicalHost(host, StatusCodes.Status301MovedPermanently);

        /// <summary>
        /// Redirect a request to the specified host / domain if the current host is different.
        /// </summary>
        /// <param name="options">The rewrite options.</param>
        /// <param name="host">The canonical host (eg. "domain.com").</param>
        public static RewriteOptions AddRedirectToCanonicalHost(this RewriteOptions options, string host)
            => options.AddRedirectToCanonicalHost(host, StatusCodes.Status302Found);

        /// <summary>
        /// Redirect a request to the specified host / domain if the current host is different.
        /// </summary>
        /// <param name="options">The rewrite options.</param>
        /// <param name="host">The canonical host (eg. "domain.com").</param>
        /// <param name="statusCode">The status code to add to the response.</param>
        public static RewriteOptions AddRedirectToCanonicalHost(this RewriteOptions options, string host, int statusCode)
        {
            options.Rules.Add(new RedirectToCanonicalHostRule { Host = host, StatusCode = statusCode });
            return options;
        }
    }
}