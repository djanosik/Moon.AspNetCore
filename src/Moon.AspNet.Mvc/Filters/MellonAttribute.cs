using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;

namespace Moon.AspNet.Mvc.Filters
{
    /// <summary>
    /// Protects action methods from being accessed by foreign people.
    /// </summary>
    public class MellonAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Protects action methods from being accessed by foreign people.
        /// </summary>
        /// <param name="context">The executing context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor.Name == "Mellon")
            {
                context.HttpContext.Response.Cookies.Append("Mellon", "Yep");
            }
            else if (!context.HttpContext.Request.Cookies.ContainsKey("Mellon"))
            {
                context.Result = new HttpNotFoundResult();
            }
        }
    }
}