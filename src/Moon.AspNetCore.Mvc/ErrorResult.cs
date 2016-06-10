using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moon.AspNetCore.Mvc
{
    /// <summary>
    /// A <see cref="StatusCodeResult" /> that when executed will produce an Error (500) response.
    /// </summary>
    public class ErrorResult : StatusCodeResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResult" /> class.
        /// </summary>
        public ErrorResult()
            : base(StatusCodes.Status500InternalServerError)
        {
        }
    }
}