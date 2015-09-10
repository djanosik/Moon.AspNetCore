using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;
using Microsoft.Framework.WebEncoders;

namespace Moon.AspNet.Authentication.Basic
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
        /// <param name="configureOptions">The middleware configuration.</param>
        public BasicAuthenticationMiddleware(RequestDelegate next, IOptions<BasicAuthenticationOptions> options, ILoggerFactory loggerFactory,
            IUrlEncoder encoder, ConfigureOptions<BasicAuthenticationOptions> configureOptions)
            : base(next, options, loggerFactory, encoder, configureOptions)
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