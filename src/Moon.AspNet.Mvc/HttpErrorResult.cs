using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace Moon.AspNet.Mvc
{
    /// <summary>
    /// A <see cref="HttpStatusCodeResult" /> that when executed will produce an Error (500) response.
    /// </summary>
    public class HttpErrorResult : HttpStatusCodeResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpErrorResult" /> class.
        /// </summary>
        public HttpErrorResult()
            : base(StatusCodes.Status500InternalServerError)
        {
        }
    }
}