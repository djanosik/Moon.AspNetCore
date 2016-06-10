using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moon.AspNetCore.Mvc
{
    /// <summary>
    /// A <see cref="StatusCodeResult" /> that when executed will produce a Forbidden (403) response.
    /// </summary>
    public class ForbiddenResult : StatusCodeResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenResult" /> class.
        /// </summary>
        public ForbiddenResult()
            : base(StatusCodes.Status403Forbidden)
        {
        }
    }
}