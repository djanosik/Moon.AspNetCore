using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace Moon.AspNet.Mvc
{
    /// <summary>
    /// A <see cref="HttpStatusCodeResult" /> that when executed will produce a Forbidden (403) response.
    /// </summary>
    public class HttpForbiddenResult : HttpStatusCodeResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpForbiddenResult" /> class.
        /// </summary>
        public HttpForbiddenResult()
            : base(StatusCodes.Status403Forbidden)
        {
        }
    }
}