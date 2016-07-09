using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Moon.AspNetCore.Mvc
{
    /// <summary>
    /// Renders contents of a view to string.
    /// </summary>
    public class ViewRenderer
    {
        readonly IActionContextAccessor actionContextAccessor;
        readonly IHttpContextAccessor httpContextAccessor;
        readonly IServiceProvider serviceProvider;
        readonly ITempDataProvider tempDataProvider;
        readonly ICompositeViewEngine viewEngine;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewRenderer" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="actionContextAccessor">The action context accessor.</param>
        /// <param name="tempDataProvider">The temporary data provider.</param>
        /// <param name="viewEngine">The view engine.</param>
        public ViewRenderer(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionContextAccessor, ITempDataProvider tempDataProvider, ICompositeViewEngine viewEngine)
        {
            this.serviceProvider = serviceProvider;
            this.httpContextAccessor = httpContextAccessor;
            this.actionContextAccessor = actionContextAccessor;
            this.tempDataProvider = tempDataProvider;
            this.viewEngine = viewEngine;
        }

        /// <summary>
        /// Renders contents of the specified view to string.
        /// </summary>
        /// <param name="name">The name of the view to render.</param>
        public Task<string> RenderAsync(string name)
            => RenderAsync<dynamic>(name, null);

        /// <summary>
        /// Renders contents of the specified view to string.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="name">The name of the view to render.</param>
        /// <param name="model">The view model.</param>
        public async Task<string> RenderAsync<TModel>(string name, TModel model)
        {
            var actionContext = actionContextAccessor.ActionContext ?? CreateActionContext();

            if (httpContextAccessor.HttpContext == null)
            {
                httpContextAccessor.HttpContext = actionContext.HttpContext;
            }

            var result = viewEngine.FindView(actionContext, name, true);

            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext,
                    result.View,
                    new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = model },
                    new TempDataDictionary(httpContextAccessor.HttpContext, tempDataProvider), output,
                    new HtmlHelperOptions()
                );

                await result.View.RenderAsync(viewContext).ConfigureAwait(false);
                return output.ToString();
            }
        }

        ActionContext CreateActionContext()
            => new ActionContext(new FakeHttpContext(serviceProvider), new RouteData(), new ActionDescriptor());

        public sealed class FakeHttpContext : DefaultHttpContext
        {
            public FakeHttpContext(IServiceProvider services)
            {
                RequestServices = services;
            }
        }
    }
}