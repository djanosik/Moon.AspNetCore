using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Moon.AspNetCore.Authentication.Basic
{
    /// <summary>
    /// The Basic authentication middleware.
    /// </summary>
    public class BasicAuthenticationMiddleware : AuthenticationMiddleware<BasicAuthenticationOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicAuthenticationMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next middleware.</param>
        /// <param name="options">The middleware options.</param>
        /// <param name="loggerFactory">The <see cref="ILogger" /> factory.</param>
        /// <param name="encoder">The URL encoder.</param>
        public BasicAuthenticationMiddleware(RequestDelegate next, IOptions<BasicAuthenticationOptions> options, ILoggerFactory loggerFactory, UrlEncoder encoder)
            : base(next, options, loggerFactory, encoder)
        {
            if (string.IsNullOrEmpty(Options.Realm))
            {
                Options.Realm = BasicAuthenticationDefaults.Realm;
            }
        }

        /// <summary>
        /// Returns the Basic authentication handler.
        /// </summary>
        protected override AuthenticationHandler<BasicAuthenticationOptions> CreateHandler()
            => new BasicAuthenticationHandler();
    }
}