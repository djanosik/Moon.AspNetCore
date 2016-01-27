using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Abstractions;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.ViewEngines;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Routing;

namespace Moon.AspNet.Mvc
{
    /// <summary>
    /// Renders contents of a view to string.
    /// </summary>
    public class ViewRenderer
    {
        readonly IServiceProvider serviceProvider;
        readonly IHttpContextAccessor httpContextAccessor;
        readonly IActionContextAccessor actionContextAccessor;
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
            var actionContext = actionContextAccessor.ActionContext;

            if (actionContext == null)
            {
                actionContext = new ActionContext(new FakeHttpContext(serviceProvider),
                    new RouteData(), new ActionDescriptor());
            }

            if (httpContextAccessor.HttpContext == null)
            {
                httpContextAccessor.HttpContext = actionContext.HttpContext;
            }

            var result = viewEngine.FindView(actionContext, name);

            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext,
                    result.View,
                    new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = model },
                    new TempDataDictionary(httpContextAccessor, tempDataProvider), output,
                    new HtmlHelperOptions()
                );

                await result.View.RenderAsync(viewContext).ConfigureAwait(false);
                return output.ToString();
            }
        }

        public class FakeHttpContext : DefaultHttpContext
        {
            public FakeHttpContext(IServiceProvider services)
            {
                ApplicationServices = services;
                RequestServices = services;
            }
        }
    }
}