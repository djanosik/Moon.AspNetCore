using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Moon.AspNetCore.Authentication.MSOFBA
{
    /// <summary>
    /// The MS-OFBA authentication middleware.
    /// </summary>
    public class MSOFBAuthenticationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly MSOFBAuthenticationOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="MSOFBAuthenticationMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next middleware.</param>
        /// <param name="options">The middleware options.</param>
        public MSOFBAuthenticationMiddleware(RequestDelegate next, IOptions<MSOFBAuthenticationOptions> options)
        {
            this.next = next;
            this.options = options.Value;

            if (!this.options.LoginPath.HasValue)
            {
                this.options.LoginPath = CookieAuthenticationDefaults.LoginPath;
            }
        }

        /// <summary>
        /// Processes a request and enables the MS-OFBA authentication.
        /// </summary>
        /// <param name="context">The HTTP settings.</param>
        public Task Invoke(HttpContext context)
        {
            if (!IsOFBAAccepted(context.Request))
            {
                return next(context);
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
                
                return Task.CompletedTask;
            }
            else
            {
                return next(context);
            }
        }

        private string ToAbsolute(HttpRequest request, PathString relativeUrl)
            => $"{request.Scheme}://{request.Host.ToUriComponent()}{relativeUrl}";

        private bool IsOFBAAccepted(HttpRequest request)
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