using System;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.TagHelpers;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Razor.TagHelpers;
using Moon.IO;

namespace Moon.AspNet.Mvc.TagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper" /> implementation targeting &lt;a&gt; elements. It appends a CSS
    /// class to &lt;a&gt; element when it is active.
    /// </summary>
    [HtmlTargetElement("a", Attributes = ActiveClassAttributeName)]
    public class ActiveClassTagHelper : TagHelper
    {
        const string ActiveClassAttributeName = "asp-active-class";

        readonly StringComparison comparison = StringComparison.OrdinalIgnoreCase;

        /// <summary>
        /// Gets the order the tag helper will run in.
        /// </summary>
        public override int Order
            => -999;

        /// <summary>
        /// Gets or sets the CSS class to append to &lt;a&gt; element when it is active.
        /// </summary>
        [HtmlAttributeName(ActiveClassAttributeName)]
        public string ActiveClass { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Microsoft.AspNet.Mvc.Rendering.ViewContext" /> for the
        /// current request.
        /// </summary>
        [HtmlAttributeNotBound, ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Appends an <see cref="ActiveClass" /> CSS class if it is not <c>null</c> and if the
        /// &lt;a&gt; element is active.
        /// </summary>
        /// <param name="context">The helper context.</param>
        /// <param name="output">The helper output.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var attributes = output.Attributes;

            if (CanBeActive(attributes) && IsActive(attributes["href"]))
            {
                var link = new TagBuilder("a");
                link.AddCssClass(ActiveClass);
                output.MergeAttributes(link);
            }
        }

        bool CanBeActive(TagHelperAttributeList attributes)
            => ActiveClass != null && attributes.ContainsName("href") && attributes["href"].Value != null;

        bool IsActive(TagHelperAttribute href)
        {
            var hrefPath = Pathy.Normalize(href.Value.ToString());
            var currentPath = ViewContext.HttpContext.Request.Path.Value;

            if (!hrefPath.Equals("/", comparison))
            {
                return currentPath.StartsWith(hrefPath, comparison);
            }

            return currentPath.Equals("/", comparison);
        }
    }
}