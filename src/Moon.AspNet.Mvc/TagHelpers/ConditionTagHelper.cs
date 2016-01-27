﻿using Microsoft.AspNet.Razor.TagHelpers;

namespace Moon.AspNet.Mvc.TagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper" /> hiding an element when the condition is not satisfied.
    /// </summary>
    [HtmlTargetElement(Attributes = ConditionAttributeName)]
    public class ConditionTagHelper : TagHelper
    {
        const string ConditionAttributeName = "asp-if";

        /// <summary>
        /// Gets or sets the condition to determine whether the element should be visible.
        /// </summary>
        [HtmlAttributeName(ConditionAttributeName)]
        public bool Condition { get; set; }

        /// <summary>
        /// Hides an element when the <see cref="Condition" /> is <c>false</c>.
        /// </summary>
        /// <param name="context">The helper context.</param>
        /// <param name="output">The helper output.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!Condition)
            {
                output.SuppressOutput();
            }
        }
    }
}