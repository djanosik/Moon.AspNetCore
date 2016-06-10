using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Moon.AspNetCore.Mvc.TagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper" /> implementation targeting &lt;script&gt; elements. It provides
    /// better support for WebPack hot loading.
    /// </summary>
    [HtmlTargetElement("script", Attributes = HotSrcAttributeName)]
    [HtmlTargetElement("script", Attributes = HotPortAttributeName)]
    public class HotLoadingTagHelper : TagHelper
    {
        const string HotSrcAttributeName = "asp-hot-src";
        const string HotPortAttributeName = "asp-hot-port";

        readonly IHostingEnvironment host;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotLoadingTagHelper" /> class.
        /// </summary>
        /// <param name="host">The hosting environment.</param>
        public HotLoadingTagHelper(IHostingEnvironment host)
        {
            this.host = host;
        }

        /// <summary>
        /// Gets the order the tag helper will run in.
        /// </summary>
        public override int Order
            => -999;

        /// <summary>
        /// Gets or sets the file name to use in development (it will be loaded from WebPack
        /// development server).
        /// </summary>
        [HtmlAttributeName(HotSrcAttributeName)]
        public string HotSrc { get; set; }

        /// <summary>
        /// Gets or sets the port the WebPack development server is listening on.
        /// </summary>
        [HtmlAttributeName(HotPortAttributeName)]
        public int HotPort { get; set; } = 8080;

        /// <summary>
        /// Appends an <see cref="ActiveClass" /> CSS class if it is not <c>null</c> and if the
        /// &lt;a&gt; element is active.
        /// </summary>
        /// <param name="context">The helper context.</param>
        /// <param name="output">The helper output.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (HotSrc != null && host.IsDevelopment())
            {
                output.TagName = "script";
                output.Attributes.SetAttribute("src", $"http://localhost:{HotPort}/{HotSrc.TrimStart('~', '/', '\\')}");
            }
        }
    }
}