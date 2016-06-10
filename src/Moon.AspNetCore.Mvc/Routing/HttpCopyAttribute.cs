using Microsoft.AspNetCore.Mvc.Routing;

namespace Moon.AspNetCore.Mvc
{
    /// <summary>
    /// Identifies an action that only supports the HTTP COPY method.
    /// </summary>
    public class HttpCopyAttribute : HttpMethodAttribute
    {
        static readonly string[] methods = { "COPY" };

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpCopyAttribute" /> class.
        /// </summary>
        public HttpCopyAttribute()
            : base(methods)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpCopyAttribute" /> class.
        /// </summary>
        /// <param name="template">The route template. May not be null.</param>
        public HttpCopyAttribute(string template)
            : base(new[] { "MOVE" }, template)
        {
        }
    }
}