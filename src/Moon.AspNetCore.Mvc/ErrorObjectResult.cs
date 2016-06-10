using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moon.AspNetCore.Mvc
{
    /// <summary>
    /// An <see cref="ObjectResult" /> that when executed will produce an Error (500) response.
    /// </summary>
    public class ErrorObjectResult : ObjectResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorObjectResult" /> class.
        /// </summary>
        /// <param name="error">The error to be returned to the client.</param>
        public ErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}