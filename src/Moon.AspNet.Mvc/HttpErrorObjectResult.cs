using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace Moon.AspNet.Mvc
{
    /// <summary>
    /// An <see cref="ObjectResult" /> that when executed will produce an Error (500) response.
    /// </summary>
    public class HttpErrorObjectResult : ObjectResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpErrorObjectResult" /> class.
        /// </summary>
        /// <param name="error">The error to be returned to the client.</param>
        public HttpErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}