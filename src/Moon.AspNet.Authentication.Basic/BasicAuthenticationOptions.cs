using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Http;

namespace Moon.AspNet.Authentication.Basic
{
    /// <summary>
    /// Contains the options used by the <see cref="BasicAuthenticationMiddleware" />.
    /// </summary>
    public class BasicAuthenticationOptions : AuthenticationOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicAuthenticationOptions" /> class.
        /// </summary>
        public BasicAuthenticationOptions()
        {
            Realm = BasicAuthenticationDefaults.Realm;
            LoginPath = CookieAuthenticationDefaults.LoginPath;
            AuthenticationScheme = BasicAuthenticationDefaults.AuthenticationScheme;
            ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            Events = new BasicAuthenticationEvents();
            AutomaticAuthentication = true;
        }

        /// <summary>
        /// Gets or sets the Realm string, typically displayed in login dialog of the client application.
        /// </summary>
        public string Realm { get; set; }

        /// <summary>
        /// Gets or sets the path to login URL which is used by <see cref="CookieAuthenticationMiddleware" />.
        /// </summary>
        /// <remarks>
        /// This property is used to detect the case when Cookie Authentication fails in a client
        /// that doesn't support it, and thus we need to issue Basic authentication.
        /// </remarks>
        public PathString LoginPath { get; set; }

        /// <summary>
        /// Gets or sets the parameter name used to pass the return URL.
        /// </summary>
        public string ReturnUrlParameter { get; set; }

        /// <summary>
        /// Gets or sets delegates called during Basic authentication process.
        /// </summary>
        public IBasicAuthenticationEvents Events { get; set; }
    }
}