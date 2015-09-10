using System;
using System.IO;
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
    /// <remarks>
    /// Provides fallback to Basic authentication if HTTP response code 302 has been issued after an
    /// attempt to authenticate using other authentication middleware (i.e. cookie) that isn't
    /// supported by host.
    /// </remarks>
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        protected override async Task<AuthenticationTicket> HandleAuthenticateAsync()
        {
            AuthenticationTicket ticket = null;

            try
            {
                var header = Request.Headers["Authorization"];

                var isBasicAuth = string.Equals(header?.Substring(0, 5), "basic", StringComparison.OrdinalIgnoreCase);

                if (!isBasicAuth)
                {
                    return null;
                }

                var credentials = DecodeCredentials(header);

                var context = new BasicSignInContext(Context, Options, credentials[0], credentials[1]);
                await Options.Events.SignInAsync(context);

                if (context.Principal == null)
                {
                    return null;
                }

                var props = new AuthenticationProperties();
                ticket = new AuthenticationTicket(context.Principal, props, Options.AuthenticationScheme);
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

        protected override Task<bool> HandleUnauthorizedAsync(ChallengeContext context)
        {
            if (Response.StatusCode == 302)
            {
                var location = Response.Headers["Location"];

                if (IsLoginUrl(location) && IsNonBrowserRequest())
                {
                    RewriteUnauthorizedResponse();
                }
                else if (MightBeWebDavRequest())
                {
                    // WebDAV request is sent while Cookie authentication is used. XHR request can
                    // not display the login page or capture 302 response code. In this case,
                    // instead of "302 Found" we send 278 response code, so XHR can process it and
                    // redirect to the login page.

                    Response.StatusCode = 278;

                    // Also change the return Url to Referer, as it points to the page on which XHR
                    // resides, while original Url points to some WebDAV url.

                    var loginPath = Options.LoginPath;
                    var referrer = Uri.EscapeDataString(Request.Headers["Referer"]);
                    var returnUrlParameter = Options.ReturnUrlParameter;

                    Response.Headers["Location"] = $"{loginPath}?{returnUrlParameter}={referrer}";
                }
            }
            else if (Response.StatusCode == 401)
            {
                AddBasicAuthenticationHeader();
            }

            return Task.FromResult(true);
        }

        string[] DecodeCredentials(string header)
        {
            var bytes = Convert.FromBase64String(header.Substring(6));
            var credentials = Encoding.UTF8.GetString(bytes).Split(':');
            var userName = credentials[0];

            // Win Vista sends userName in the form of DOMAIN\User

            var delimiterIndex = userName.IndexOf('\\');

            if (delimiterIndex != -1)
            {
                credentials[0] = userName.Remove(0, delimiterIndex + 1);
            }

            return credentials;
        }

        bool IsLoginUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            var pos = url.IndexOf('?');

            if (pos >= 0)
            {
                url = url.Substring(0, pos);
            }

            return url.EndsWith(Options.LoginPath, StringComparison.OrdinalIgnoreCase);
        }

        bool IsNonBrowserRequest()
        {
            var userAgent = Request.Headers["User-Agent"];
            return userAgent == null || userAgent.IndexOf("Mozilla", StringComparison.OrdinalIgnoreCase) < 0;
        }

        void RewriteUnauthorizedResponse()
        {
            Response.StatusCode = 401;

            // Null out the Location header from the 302 response so that it doesn't get transmitted
            // to the client unnecessarily.

            Response.Headers.Remove("Location");

            // Change the response entity to reflect the right message. If we don't clear out the
            // response and re-write it here, then the user might get the wrong message---that is,
            // she might see an HTML page stating that the resource has been moved (which would have
            // been right for 302) when what we want to indicate is an attempt to access an
            // unauthorized resource.

            Response.Body = new MemoryStream();
            AddBasicAuthenticationHeader();
        }

        void AddBasicAuthenticationHeader()
            => Response.Headers.Append("WWW-Authenticate", $"Basic realm=\"{Options.Realm}\"");

        bool MightBeWebDavRequest()
            => Request.Method != "GET" && Request.Method != "HEAD" && Request.Method != "POST";
    }
}