using Microsoft.AspNet.Authentication;

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
            AuthenticationScheme = BasicAuthenticationDefaults.AuthenticationScheme;
            Events = new BasicAuthenticationEvents();
            AutomaticAuthentication = true;
        }

        /// <summary>
        /// Gets or sets the Realm string, typically displayed in login dialog of the client application.
        /// </summary>
        public string Realm { get; set; }

        /// <summary>
        /// Gets or sets delegates called during Basic authentication process.
        /// </summary>
        public IBasicAuthenticationEvents Events { get; set; }
    }
}