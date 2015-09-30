using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace Moon.AspNet.Mvc
{
    /// <summary>
    /// An <see cref="ObjectResult" /> that when executed will produce a Forbidden (403) response.
    /// </summary>
    public class HttpForbiddenObjectResult : ObjectResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpForbiddenObjectResult" /> class.
        /// </summary>
        /// <param name="error">The value to be returned to the client.</param>
        public HttpForbiddenObjectResult(object value)
            : base(value)
        {
            StatusCode = StatusCodes.Status403Forbidden;
        }
    }
}