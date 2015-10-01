using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.ModelBinding;
using Moon.OData;
using Moon.Reflection;

namespace Moon.AspNet.OData
{
    /// <summary>
    /// <see cref="IModelBinder" /> implementation to bind models of type <see cref="ODataQuery{TEntity}" />.
    /// </summary>
    public class ODataQueryModelBinder : IModelBinder
    {
        readonly IEnumerable<IPrimitiveType> primitives;

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataQueryModelBinder" /> class.
        /// </summary>
        /// <param name="primitives">An enumeration of primitive types.</param>
        public ODataQueryModelBinder(IEnumerable<IPrimitiveType> primitives)
        {
            this.primitives = primitives;
        }

        /// <summary>
        /// Binds an <see cref="ODataQuery{TEntity}" /> parameter if possible.
        /// </summary>
        /// <param name="bindingContext">The binding context.</param>
        public Task<ModelBindingResult> BindModelAsync(ModelBindingContext bindingContext)
        {
            ModelBindingResult result = null;
            var request = bindingContext.OperationBindingContext.HttpContext.Request;
            var modelType = bindingContext.ModelType;

            if (modelType.GetGenericTypeDefinition() == typeof(ODataQuery<>))
            {
                var model = Class.Create(modelType, GetQueryOptions(request), primitives);
                result = new ModelBindingResult(model, bindingContext.ModelName, true);
            }

            return Task.FromResult(result);
        }

        static IDictionary<string, string> GetQueryOptions(HttpRequest request)
        {
            Requires.NotNull(request, nameof(request));

            return request.Query.ToDictionary(p => p.Key, p => p.Value.FirstOrDefault());
        }
    }
}