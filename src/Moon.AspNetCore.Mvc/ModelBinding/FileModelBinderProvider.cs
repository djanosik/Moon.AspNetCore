using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Moon.AspNetCore.Mvc.ModelBinding
{
    /// <summary>
    /// An <see cref="IModelBinderProvider" /> for binding a file to byte array.
    /// </summary>
    public class FileModelBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// Returns an instance of the <see cref="FileModelBinder" />.
        /// </summary>
        /// <param name="context">The instance context.</param>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            var modelType = context.Metadata.ModelType;
            return modelType == typeof(byte[]) ? new FileModelBinder() : null;
        }
    }
}