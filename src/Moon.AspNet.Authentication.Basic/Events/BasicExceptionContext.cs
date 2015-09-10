using System;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Http;

namespace Moon.AspNet.Authentication.Basic
{
    /// <summary>
    /// Context object used to control flow of Basic authentication.
    /// </summary>
    public class BasicExceptionContext : BaseContext<BasicAuthenticationOptions>
    {
        /// <summary>
        /// Creates a new instance of the context object.
        /// </summary>
        /// <param name="context">The HTTP request context.</param>
        /// <param name="options">The middleware options.</param>
        /// <param name="exception">The exception thrown.</param>
        /// <param name="ticket">The current ticket, if any.</param>
        public BasicExceptionContext(HttpContext context, BasicAuthenticationOptions options, Exception exception, AuthenticationTicket ticket)
            : base(context, options)
        {
            Exception = exception;
            Ticket = ticket;
            Rethrow = true;
        }

        /// <summary>
        /// Gets the exception thrown.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Gets or sets the current authentication ticket, if any. In the AuthenticateAsync code
        /// path, if the given exception is not re-thrown then this ticket will be returned to the
        /// application. The ticket may be replaced if needed.
        /// </summary>
        public AuthenticationTicket Ticket { get; set; }

        /// <summary>
        /// Gets or sets whether the exception should be re-thrown (default).
        /// </summary>
        public bool Rethrow { get; set; }
    }
}