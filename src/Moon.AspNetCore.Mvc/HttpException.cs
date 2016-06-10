using System;
using Microsoft.AspNetCore.Mvc;

namespace Moon.AspNetCore.Mvc
{
    /// <summary>
    /// The HTTP status exception.
    /// </summary>
    public class HttpException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException" /> class.
        /// </summary>
        /// <param name="result">The action result to execute.</param>
        public HttpException(IActionResult result)
            : base("An HTTP error occurred.")
        {
            Result = result;
        }

        /// <summary>
        /// Gets the action result to execute.
        /// </summary>
        public IActionResult Result { get; }
    }
}