using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace Moon.AspNet.Mvc
{
    /// <summary>
    /// An <see cref="ObjectResult" /> that when executed will produce an Unprocessable Entity (422) response.
    /// </summary>
    public class UnprocessableEntityObjectResult : ObjectResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnprocessableEntityObjectResult" /> class.
        /// </summary>
        /// <param name="modelState">A dictionary containing the validation errors.</param>
        public UnprocessableEntityObjectResult(ModelStateDictionary modelState)
            : base(new SerializableError(modelState))
        {
            StatusCode = 422;
        }
    }
}