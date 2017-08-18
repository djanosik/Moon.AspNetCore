using Microsoft.AspNetCore.Authentication;

namespace Moon.AspNetCore.Authentication.Basic
{
    /// <summary>
    /// Contains the options used by the <see cref="BasicAuthenticationHandler" />.
    /// </summary>
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicAuthenticationOptions" /> class.
        /// </summary>
        public BasicAuthenticationOptions()
        {
            Events = new BasicAuthenticationEvents();
        }

        /// <summary>
        /// Gets or sets the Realm string, typically displayed in login dialog of the client application.
        /// </summary>
        public string Realm { get; set; } = BasicAuthenticationDefaults.Realm;

        /// <summary>
        /// Gets or sets delegates called during Basic authentication process.
        /// </summary>
        public new BasicAuthenticationEvents Events
        {
            get => (BasicAuthenticationEvents)base.Events;
            set => base.Events = value;
        }
    }
}