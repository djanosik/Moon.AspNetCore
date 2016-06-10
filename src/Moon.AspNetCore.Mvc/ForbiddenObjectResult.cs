using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moon.AspNetCore.Mvc
{
    /// <summary>
    /// An <see cref="ObjectResult" /> that when executed will produce a Forbidden (403) response.
    /// </summary>
    public class ForbiddenObjectResult : ObjectResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenObjectResult" /> class.
        /// </summary>
        /// <param name="value">The value to be returned to the client.</param>
        public ForbiddenObjectResult(object value)
            : base(value)
        {
            StatusCode = StatusCodes.Status403Forbidden;
        }
    }
}