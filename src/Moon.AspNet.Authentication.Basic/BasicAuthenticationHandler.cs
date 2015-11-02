using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Http.Features.Authentication;

namespace Moon.AspNet.Authentication.Basic
{
    /// <summary>
    /// The Basic authentication handler.
    /// </summary>
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        /// <summary>
        /// Handles the authentication by checking the "Authorization" header.
        /// </summary>
        protected override async Task<AuthenticationTicket> HandleAuthenticateAsync()
        {
            AuthenticationTicket ticket = null;

            try
            {
                var header = Request.Headers["Authorization"];

                if (IsBasicAuthentication(header))
                {
                    var credentials = DecodeCredentials(header);

                    var context = new BasicSignInContext(Context, Options, credentials.UserName,
                        credentials.Password);

                    await Options.Events.SignInAsync(context);

                    if (context.Principal != null)
                    {
                        ticket = new AuthenticationTicket(context.Principal, new AuthenticationProperties(),
                            Options.AuthenticationScheme);
                    }
                }

                return ticket;
            }
            catch (Exception ex)
            {
                var context = new BasicExceptionContext(Context, Options, ex, ticket);

                Options.Events.Exception(context);

                if (!context.Rethrow)
                {
                    return context.Ticket;
                }

                throw;
            }
        }

        /// <summary>
        /// Handles the unauthorized response by settings the "WWW-Authenticate" header.
        /// </summary>
        /// <param name="context">The challenge context.</param>
        protected override Task<bool> HandleUnauthorizedAsync(ChallengeContext context)
        {
            Response.Headers.Add("WWW-Authenticate", $"Basic realm=\"{Options.Realm}\"");
            return base.HandleUnauthorizedAsync(context);
        }

        bool IsBasicAuthentication(string header)
            => "basic".Equals(header?.Substring(0, 5), StringComparison.OrdinalIgnoreCase);

        NetworkCredential DecodeCredentials(string header)
        {
            var bytes = Convert.FromBase64String(header.Substring(6));
            var parts = Encoding.UTF8.GetString(bytes).Split(':');
            var slashIndex = parts[0].IndexOf('\\');

            return new NetworkCredential
            {
                UserName = slashIndex >= 0 ? parts[0].Remove(0, slashIndex + 1) : parts[0],
                Password = parts[1]
            };
        }
    }
}