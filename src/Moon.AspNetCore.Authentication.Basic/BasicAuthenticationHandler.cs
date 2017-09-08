using System;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Moon.AspNetCore.Authentication.Basic
{
    /// <summary>
    /// The Basic authentication handler.
    /// </summary>
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicAuthenticationHandler" /> class.
        /// </summary>
        public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        /// <summary>
        /// Handles the authentication by checking the "Authorization" header.
        /// </summary>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var header = Request.Headers["Authorization"];

                if (IsBasicAuthentication(header))
                {
                    var credentials = DecodeCredentials(header);
                    var context = new BasicSignInContext(Context, Scheme, Options, credentials);
                    await Options.Events.SignInAsync(context);

                    if (context.Principal != null)
                    {
                        var ticket = new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
                        return AuthenticateResult.Success(ticket);
                    }

                    return AuthenticateResult.Fail("No principal.");
                }

                return AuthenticateResult.NoResult();
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex);
            }
        }

        /// <summary>
        /// Handles the unauthorized response by settings the "WWW-Authenticate" header.
        /// </summary>
        /// <param name="properties">The authentication properties.</param>
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Headers.Add("WWW-Authenticate", $"Basic realm=\"{Options.Realm}\"");
            return base.HandleChallengeAsync(properties);
        }

        private bool IsBasicAuthentication(string header)
            => "basic".Equals(header?.Substring(0, 5), StringComparison.OrdinalIgnoreCase);

        private NetworkCredential DecodeCredentials(string header)
        {
            var bytes = Convert.FromBase64String(header.Substring(6));
            var parts = Encoding.UTF8.GetString(bytes).Split(':');
            var slashIndex = parts[0].IndexOf('\\');

            return new NetworkCredential {
                UserName = slashIndex >= 0 ? parts[0].Remove(0, slashIndex + 1) : parts[0],
                Password = parts[1]
            };
        }
    }
}