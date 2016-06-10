using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Moon.AspNetCore.Mvc.Filters
{
    /// <summary>
    /// An action filter which sets <see cref="ActionExecutedContext.Result" /> to an
    /// <see cref="HttpException.Result" /> if the exception type is <see cref="HttpException" />.
    /// This filter runs immediately after the action.
    /// </summary>
    class HttpExceptionActionFilter : IActionFilter, IOrderedFilter
    {
        /// <summary>
        /// Gets the order value for determining the order of execution of filters.
        /// </summary>
        public int Order { get; set; } = int.MaxValue - 10;

        /// <summary>
        /// Sets <see cref="ActionExecutedContext.Result" /> to an <see cref="HttpException.Result" />.
        /// </summary>
        /// <param name="context">The action context.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var httpException = context.Exception as HttpException;

            if (httpException != null && httpException.Result != null)
            {
                var objectResult = httpException.Result as ObjectResult;
                var errorMessage = objectResult?.Value as ErrorMessage;

                if (errorMessage != null)
                {
                    context.HttpContext.Response.Headers.Add("Error-Message", new StringValues(errorMessage.Message));
                }

                context.Result = httpException.Result;
                context.ExceptionHandled = true;
            }
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            // NOOP
        }
    }
}