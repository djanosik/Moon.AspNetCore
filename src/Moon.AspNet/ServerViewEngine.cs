using System.Collections.Generic;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.AspNet.Mvc.Razor.OptionDescriptors;

namespace Moon.AspNet
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
        /// <param name="viewLocationExpanderProvider">The view location expander.</param>
        /// <param name="viewLocationCache">The view location cache.</param>
        public ServerViewEngine(IRazorPageFactory pageFactory, IRazorViewFactory viewFactory, IViewLocationExpanderProvider viewLocationExpanderProvider, IViewLocationCache viewLocationCache)
            : base(pageFactory, viewFactory, viewLocationExpanderProvider, viewLocationCache)
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