using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Moon.AspNetCore.Authentication.MSOFBA
{
    /// <summary>
    /// The MS-OFBA authentication handler.
    /// </summary>
    public class MSOFBAuthenticationHandler : AuthenticationHandler<MSOFBAuthenticationOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MSOFBAuthenticationHandler" /> class.
        /// </summary>
        public MSOFBAuthenticationHandler(IOptionsMonitor<MSOFBAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        /// <summary>
        /// Handles the authentication by checking the "Authorization" header.
        /// </summary>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.FromResult(AuthenticateResult.Success(null));
        }

        /// <summary>
        /// Handles the unauthorized response by settings the "WWW-Authenticate" header.
        /// </summary>
        /// <param name="properties">The authentication properties.</param>
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            if (IsOFBAAccepted() && !IsUserAuthenticated())
            {
                var loginUrl = ToAbsolute(Request, Options.LoginPath);
                var sucessUrl = ToAbsolute(Request, Options.LoginSuccessPath);

                Response.Headers.Add("X-FORMS_BASED_AUTH_REQUIRED", new[] { $"{loginUrl}?{Options.ReturnUrlParameter}={sucessUrl}" });
                Response.Headers.Add("X-FORMS_BASED_AUTH_DIALOG_SIZE", new[] { $"{Options.DialogSize.Width}x{Options.DialogSize.Height}" });
                Response.Headers.Add("X-FORMS_BASED_AUTH_RETURN_URL", new[] { sucessUrl });
                Response.StatusCode = 403;
            }

            return base.HandleChallengeAsync(properties);
        }

        private string ToAbsolute(HttpRequest request, PathString relativeUrl)
            => $"{request.Scheme}://{request.Host.ToUriComponent()}{relativeUrl}";

        private bool IsUserAuthenticated()
            => Context.User != null && Context.User.Identity.IsAuthenticated;

        private bool IsOFBAAccepted()
        {
            var ofbaAccepted = Request.Headers["X-FORMS_BASED_AUTH_ACCEPTED"];

            if (string.Equals(ofbaAccepted, "T", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            var userAgent = Request.Headers["User-Agent"];

            if (userAgent.Count >= 1 && userAgent[0].Contains("Microsoft Office"))
            {
                return true;
            }

            return false;
        }
    }
}