using Microsoft.AspNet.Mvc;

namespace Moon.AspNet.Mvc
{
    /// <summary>
    /// An <see cref="ErrorResult" /> that when executed will produce a Server Error (500) response.
    /// </summary>
    public class ErrorResult : ObjectResult
    {
        /// <summary>
        /// Creates a new <see cref="BadRequestObjectResult" /> instance.
        /// </summary>
        /// <param name="errorMessage">The error message to be returned to the client.</param>
        public ErrorResult(string errorMessage)
            : base(new { Error = errorMessage })
        {
            Requires.NotNullOrWhiteSpace(errorMessage, nameof(errorMessage));

            StatusCode = 500;
        }
    }
}