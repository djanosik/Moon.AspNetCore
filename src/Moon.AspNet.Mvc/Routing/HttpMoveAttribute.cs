﻿using Microsoft.AspNet.Mvc.Routing;

namespace Moon.AspNet.Mvc
{
    /// <summary>
    /// Identifies an action that only supports the HTTP MOVE method.
    /// </summary>
    public class HttpMoveAttribute : HttpMethodAttribute
    {
        static readonly string[] methods = { "MOVE" };

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMoveAttribute" /> class.
        /// </summary>
        public HttpMoveAttribute()
            : base(methods)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMoveAttribute" /> class.
        /// </summary>
        /// <param name="template">The route template. May not be null.</param>
        public HttpMoveAttribute(string template)
            : base(new[] { "MOVE" }, template)
        {
        }
    }
}