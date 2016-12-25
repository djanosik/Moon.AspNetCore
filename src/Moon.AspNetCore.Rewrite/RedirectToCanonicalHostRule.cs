using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;

namespace Moon.AspNetCore.Rewrite
{
    /// <summary>
    /// The rewrite rule redirecting to canonical host (eg. "domain.com").
    /// </summary>
    public class RedirectToCanonicalHostRule : IRule
    {
        /// <summary>
        /// Gets or sets the canonical host (eg. "domain.com").
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the status code to add to the response.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Redirects to canonical host if the current host does not match.
        /// </summary>
        /// <param name="context">The rewrite context.</param>
        public virtual void ApplyRule(RewriteContext context)
        {
            var req = context.HttpContext.Request;
            var resp = context.HttpContext.Response;

            if (!Regex.IsMatch(req.Host.Host, $"^{Regex.Escape(Host)}$"))
            {
                resp.StatusCode = StatusCode;
                resp.Headers[HeaderNames.Location] = $"{req.Scheme}://{Host}{req.PathBase}{req.Path}{req.QueryString}";
                context.Result = RuleResult.EndResponse;
            }
        }
    }
}