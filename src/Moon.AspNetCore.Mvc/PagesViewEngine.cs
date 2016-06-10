using System.Collections.Generic;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Moon.AspNetCore.Mvc
{
    /// <summary>
    /// Represents a Razor view engine that looks for views in the ~/Pages/ folder.
    /// </summary>
    public class PagesViewEngine : RazorViewEngine
    {
        static string[] areaViewLocationFormats =   {
            "/Areas/{2}/Pages/{1}/{0}.cshtml",
            "/Areas/{2}/Pages/Shared/{0}.cshtml",
            "/Pages/Shared/{0}.cshtml"
        };

        static string[] viewLocationFormats = {
            "/Pages/{1}/{0}.cshtml",
            "/Pages/Shared/{0}.cshtml"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="PagesViewEngine" /> class.
        /// </summary>
        /// <param name="pageFactory">The page factory provider.</param>
        /// <param name="pageActivator">The page activator.</param>
        /// <param name="htmlEncoder">The HTML encoder.</param>
        /// <param name="optionsAccessor">The view engine options accessor.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public PagesViewEngine(IRazorPageFactoryProvider pageFactory, IRazorPageActivator pageActivator, HtmlEncoder htmlEncoder, IOptions<RazorViewEngineOptions> optionsAccessor, ILoggerFactory loggerFactory)
            : base(pageFactory, pageActivator, htmlEncoder, optionsAccessor, loggerFactory)
        {
        }

        /// <summary>
        /// Gets the locations where the engine will search for views within an area.
        /// </summary>
        public override IEnumerable<string> AreaViewLocationFormats
            => areaViewLocationFormats;

        /// <summary>
        /// Gets the locations where the engine will search for views.
        /// </summary>
        public override IEnumerable<string> ViewLocationFormats
            => viewLocationFormats;
    }
}