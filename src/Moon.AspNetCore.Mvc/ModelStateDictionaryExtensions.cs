using Microsoft.AspNetCore.Mvc.ModelBinding;

// ReSharper disable once CheckNamespace

namespace Microsoft.AspNetCore.Mvc
{
    public static class ModelStateDictionaryExtensions
    {
        /// <summary>
        /// Adds the specified <paramref name="errorMessage" /> to the <see cref="ModelStateDictionary" />.
        /// </summary>
        /// <param name="modelState">The model state dictionary.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddModelError(this ModelStateDictionary modelState, string errorMessage)
            => modelState.AddModelError("", errorMessage);
    }
}