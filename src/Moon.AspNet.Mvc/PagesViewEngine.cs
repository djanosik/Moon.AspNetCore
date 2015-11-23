using System.Collections.Generic;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.OptionsModel;

namespace Moon.AspNet.Mvc
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
        /// <param name="pageFactory">The page factory.</param>
        /// <param name="viewFactory">The view factory.</param>
        /// <param name="optionsAccessor">The view engine options accessor.</param>
        /// <param name="viewLocationCache">The view location cache.</param>
        public PagesViewEngine(IRazorPageFactory pageFactory, IRazorViewFactory viewFactory, IOptions<RazorViewEngineOptions> optionsAccessor, IViewLocationCache viewLocationCache)
            : base(pageFactory, viewFactory, optionsAccessor, viewLocationCache)
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