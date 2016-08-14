using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using Moon.IO;

namespace Moon.AspNetCore.Mvc.ModelBinding
{
    /// <summary>
    /// A model binder to bind uploaded files to Byte arrays.
    /// </summary>
    public class FileModelBinder : IModelBinder
    {
        /// <summary>
        /// Binds an uploaded file to a Byte array.
        /// </summary>
        /// <param name="bindingContext">The binding context.</param>
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var file = await GetFormFile(bindingContext);

            if (file != null)
            {
                using (var stream = file.OpenReadStream())
                {
                    bindingContext.Result = ModelBindingResult.Success(await stream.ReadAllBytesAsync());
                }
            }
        }

        private async Task<IFormFile> GetFormFile(ModelBindingContext bindingContext)
        {
            var request = bindingContext.HttpContext.Request;

            if (!request.HasFormContentType)
            {
                return null;
            }

            var form = await request.ReadFormAsync();

            // If we're at the top level, then use the FieldName (parameter or property name).
            // This handles the fact that there will be nothing in the ValueProviders for this
            // parameter and so we'll do the right thing even though we 'fell-back' to the empty prefix.
            var modelName = bindingContext.IsTopLevelObject
                ? bindingContext.FieldName
                : bindingContext.ModelName;

            foreach (var file in form.Files)
            {
                ContentDispositionHeaderValue parsedContentDisposition;
                ContentDispositionHeaderValue.TryParse(file.ContentDisposition, out parsedContentDisposition);

                // If there is an <input type="file" ... /> in the form and is left blank.
                if ((parsedContentDisposition == null) || ((file.Length == 0) && string.IsNullOrEmpty(HeaderUtilities.RemoveQuotes(parsedContentDisposition.FileName))))
                {
                    continue;
                }

                var name = HeaderUtilities.RemoveQuotes(parsedContentDisposition.Name);

                if (name.Equals(modelName, StringComparison.OrdinalIgnoreCase))
                {
                    return file;
                }
            }

            return null;
        }
    }
}