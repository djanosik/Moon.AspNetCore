using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

// ReSharper disable once CheckNamespace

namespace Moon.AspNetCore.Authentication.Basic
{
    /// <summary>
    /// Context object used to control flow of Basic authentication.
    /// </summary>
    public class BasicSignInContext : PrincipalContext<BasicAuthenticationOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicSignInContext" /> class.
        /// </summary>
        public BasicSignInContext(HttpContext context, AuthenticationScheme scheme, BasicAuthenticationOptions options, NetworkCredential credentials)
            : base(context, scheme, options, null)
        {
            UserName = credentials.UserName;
            Password = credentials.Password;
        }

        /// <summary>
        /// Gets or sets the user name (login, e-mail, etc.) used.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password (secret).
        /// </summary>
        public string Password { get; set; }
    }
}