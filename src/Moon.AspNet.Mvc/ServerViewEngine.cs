using System.Collections.Generic;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.OptionsModel;

namespace Moon.AspNet.Mvc
{
    /// <summary>
    /// Represents a Razor view engine that looks for views in the ~/Server/ folder.
    /// </summary>
    public class ServerViewEngine : RazorViewEngine
    {
        static string[] areaViewLocationFormats =   {
            "/Server/Areas/{2}/{1}/{0}.cshtml",
            "/Server/Areas/{2}/Shared/{0}.cshtml",
            "/Server/Shared/{0}.cshtml"
        };

        static string[] viewLocationFormats = {
            "/Server/{1}/{0}.cshtml",
            "/Server/Shared/{0}.cshtml"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerViewEngine" /> class.
        /// </summary>
        /// <param name="pageFactory">The page factory.</param>
        /// <param name="viewFactory">The view factory.</param>
        /// <param name="optionsAccessor">The view engine options accessor.</param>
        /// <param name="viewLocationCache">The view location cache.</param>
        public ServerViewEngine(IRazorPageFactory pageFactory, IRazorViewFactory viewFactory, IOptions<RazorViewEngineOptions> optionsAccessor, IViewLocationCache viewLocationCache)
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