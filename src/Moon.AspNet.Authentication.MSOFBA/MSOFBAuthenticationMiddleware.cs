using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Moon.AspNet.Authentication.MSOFBA
{
    /// <summary>
    /// The MS-OFBA authentication middleware.
    /// </summary>
    public class MSOFBAuthenticationMiddleware
    {
        readonly RequestDelegate next;
        readonly MSOFBAuthenticationOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="MSOFBAuthenticationMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next middleware.</param>
        /// <param name="options">The middleware options.</param>
        public MSOFBAuthenticationMiddleware(RequestDelegate next, MSOFBAuthenticationOptions options)
        {
            this.next = next;
            this.options = options;
        }

        /// <summary>
        /// Processes a request and enables the MS-OFBA authentication.
        /// </summary>
        /// <param name="context">The HTTP settings.</param>
        public async Task Invoke(HttpContext context)
        {
            if (!IsOFBAAccepted(context.Request))
            {
                await next(context);
            }

            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                var loginUri = ToAbsolute(context.Request, options.LoginPath);
                var successUri = ToAbsolute(context.Request, new PathString("/"));
                var returnUrlParameter = options.ReturnUrlParameter;

                context.Response.Headers.Add("X-FORMS_BASED_AUTH_DIALOG_SIZE", new[] { $"{600}x{500}" });
                context.Response.Headers.Add("X-FORMS_BASED_AUTH_REQUIRED", new[] { $"{loginUri}?{returnUrlParameter}={successUri}" });
                context.Response.Headers.Add("X-FORMS_BASED_AUTH_RETURN_URL", new[] { successUri });
                context.Response.StatusCode = 403;
            }
        }

        string ToAbsolute(HttpRequest request, PathString relativeUrl)
            => $"{request.Scheme}://{request.Host.ToUriComponent()}{relativeUrl}";

        bool IsOFBAAccepted(HttpRequest request)
        {
            var ofbaAccepted = request.Headers["X-FORMS_BASED_AUTH_ACCEPTED"];

            if (string.Equals(ofbaAccepted, "T", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            var userAgent = request.Headers["User-Agent"];

            if (userAgent.Count >= 1 && userAgent[0].Contains("Microsoft Office"))
            {
                return true;
            }

            return false;
        }
    }
}