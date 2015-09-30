using System;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.ModelBinding;
using Moon;
using Moon.AspNet.Mvc;

namespace Microsoft.AspNet.Mvc
{
    /// <summary>
    /// <see cref="HttpResponse" /> extension methods.
    /// </summary>
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Bad Request (400) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        public static HttpException BadRequest(this HttpResponse response)
            => new HttpException(new BadRequestResult());

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Bad Request (400) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="error">The error message to be returned to the client.</param>
        public static HttpException BadRequest(this HttpResponse response, string error)
        {
            Requires.NotNullOrWhiteSpace(error, nameof(error));

            return response.BadRequest(new { error });
        }

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Bad Request (400) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="error">The error to be returned to the client.</param>
        public static HttpException BadRequest(this HttpResponse response, object error)
        {
            var disposable = error as IDisposable;

            if (disposable != null)
            {
                response.RegisterForDispose(disposable);
            }

            var result = new BadRequestObjectResult(error);
            return new HttpException(result);
        }

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces an Unauthorized (401) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        public static HttpException Unauthorized(this HttpResponse response)
            => new HttpException(new HttpUnauthorizedResult());

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Forbidden (403) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        public static HttpException Forbidden(this HttpResponse response)
            => new HttpException(new HttpForbiddenResult());

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Forbidden (403) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="message">The message to be returned to the client.</param>
        public static HttpException Forbidden(this HttpResponse response, string message)
        {
            Requires.NotNullOrWhiteSpace(message, nameof(message));

            return response.Forbidden(new { message });
        }

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Forbidden (403) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        public static HttpException Forbidden(this HttpResponse response, object value)
        {
            var disposable = value as IDisposable;

            if (disposable != null)
            {
                response.RegisterForDispose(disposable);
            }

            var result = new HttpForbiddenObjectResult(value);
            return new HttpException(result);
        }

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Not Found (404) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        public static HttpException NotFound(this HttpResponse response)
            => new HttpException(new HttpNotFoundResult());

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Not Found (404) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="message">The message to be returned to the client.</param>
        public static HttpException NotFound(this HttpResponse response, string message)
            => response.NotFound(new { message });

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Not Found (404) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="value">The value to be returned to the client.</param>
        public static HttpException NotFound(this HttpResponse response, object value)
        {
            var disposable = value as IDisposable;

            if (disposable != null)
            {
                response.RegisterForDispose(disposable);
            }

            var result = new HttpNotFoundObjectResult(value);
            return new HttpException(result);
        }

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces an Unprocessable Entity (422) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="modelState">A dictionary containing the validation errors.</param>
        public static HttpException Unprocessable(this HttpResponse response, ModelStateDictionary modelState)
            => new HttpException(new UnprocessableEntityObjectResult(modelState));

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces an Error (500) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        public static HttpException Error(this HttpResponse response)
            => new HttpException(new HttpErrorResult());

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces an Error (500) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="error">The error message to be returned to the client.</param>
        public static HttpException Error(this HttpResponse response, string error)
        {
            Requires.NotNullOrWhiteSpace(error, nameof(error));

            return response.Error(new { error });
        }

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces an Error (500) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="error">The error to be returned to the client.</param>
        public static HttpException Error(this HttpResponse response, object error)
        {
            var disposable = error as IDisposable;

            if (disposable != null)
            {
                response.RegisterForDispose(disposable);
            }

            var result = new HttpErrorObjectResult(error);
            return new HttpException(result);
        }
    }
}