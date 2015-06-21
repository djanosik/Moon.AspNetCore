using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Moon.AspNet
{
    /// <summary>
    /// Redirects request to preferred host name when not-matching host name is detected.
    /// </summary>
    public class OneHostNameMiddleware
    {
        readonly RequestDelegate next;
        readonly OneHostNameOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneHostNameMiddleware" /> class
        /// </summary>
        /// <param name="next">The next middleware.</param>
        /// <param name="options">The options.</param>
        public OneHostNameMiddleware(RequestDelegate next, OneHostNameOptions options)
        {
            this.next = next;
            this.options = options;
        }

        /// <summary>
        /// Processes a requests and redirects to the preferred host name.
        /// </summary>
        /// <param name="context">The HTTP settings.</param>
        public Task Invoke(HttpContext context)
        {
            var pattern = options.Pattern;
            var hostName = GetHostName(context);

            if (pattern.IsMatch(hostName))
            {
                return next(context);
            }

            var targetUrl = GetUrl(context);
            context.Response.Redirect(targetUrl, true);

            return Task.CompletedTask;
        }

        string GetHostName(HttpContext context)
            => context.Request.Host.Value.Split(':')[0];

        string GetUrl(HttpContext context)
        {
            var builder = new UriBuilder
            {
                Scheme = context.Request.Scheme,
                Host = options.HostName,
                Port = GetPort(context),
                Path = context.Request.Path.Value,
                Query = context.Request.QueryString.Value.TrimStart('?')
            };

            return builder.ToString();
        }

        int GetPort(HttpContext context)
        {
            var host = context.Request.Host;

            if (host.Value.Contains(":"))
            {
                return int.Parse(host.Value.Split(':')[1]);
            }

            return context.Request.IsHttps
                ? 443 : 80;
        }
    }
}