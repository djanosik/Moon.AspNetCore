using System;
using System.Net.Http;

namespace Moon.AspNet
{
    /// <summary>
    /// <see cref="HttpResponseMessage" /> extension methods.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Gets the media-type of the current response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        public static string GetMediaType(this HttpResponseMessage response)
            => response.Content.Headers.ContentType?.MediaType;

        /// <summary>
        /// Gets the URI of the Internet resource that actually responded to the request.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        public static Uri GetResponseUri(this HttpResponseMessage response)
            => response.RequestMessage.RequestUri;
    }
}