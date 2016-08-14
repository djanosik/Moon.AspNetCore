using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moon.IO;

namespace Moon.AspNetCore.Mvc.TagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper" /> implementation targeting &lt;a&gt; elements. It appends a CSS class
    /// to &lt;a&gt; element when it is active.
    /// </summary>
    [HtmlTargetElement("a", Attributes = activeClassAttributeName)]
    public class ActiveClassTagHelper : TagHelper
    {
        private const string activeClassAttributeName = "asp-active-class";
        private const StringComparison comparison = StringComparison.OrdinalIgnoreCase;

        /// <summary>
        /// Gets the order the tag helper will run in.
        /// </summary>
        public override int Order
            => -999;

        /// <summary>
        /// Gets or sets the CSS class to append to &lt;a&gt; element when it is active.
        /// </summary>
        [HtmlAttributeName(activeClassAttributeName)]
        public string ActiveClass { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Microsoft.AspNetCore.Mvc.Rendering.ViewContext" /> for the
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

        private bool CanBeActive(TagHelperAttributeList attributes)
            => (ActiveClass != null) && attributes.ContainsName("href") && (attributes["href"].Value != null);

        private bool IsActive(TagHelperAttribute href)
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