using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moon;
using Moon.AspNetCore;
using Moon.AspNetCore.Mvc;

namespace Microsoft.AspNetCore.Mvc
{
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
        /// <param name="message">The error message to be returned to the client.</param>
        public static HttpException BadRequest(this HttpResponse response, string message)
        {
            Requires.NotNullOrWhiteSpace(message, nameof(message));

            return response.BadRequest(new ErrorMessage { Message = message });
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
            => new HttpException(new UnauthorizedResult());

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Forbidden (403) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        public static HttpException Forbidden(this HttpResponse response)
            => new HttpException(new ForbiddenResult());

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Forbidden (403) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="message">The error message to be returned to the client.</param>
        public static HttpException Forbidden(this HttpResponse response, string message)
        {
            Requires.NotNullOrWhiteSpace(message, nameof(message));

            return response.Forbidden(new ErrorMessage { Message = message });
        }

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Forbidden (403) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="error">The error to be returned to the client.</param>
        public static HttpException Forbidden(this HttpResponse response, object error)
        {
            var disposable = error as IDisposable;

            if (disposable != null)
            {
                response.RegisterForDispose(disposable);
            }

            var result = new ForbiddenObjectResult(error);
            return new HttpException(result);
        }

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Not Found (404) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        public static HttpException NotFound(this HttpResponse response)
            => new HttpException(new NotFoundResult());

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Not Found (404) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="message">The error message to be returned to the client.</param>
        public static HttpException NotFound(this HttpResponse response, string message)
            => response.NotFound(new ErrorMessage { Message = message });

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces a Not Found (404) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="error">The error to be returned to the client.</param>
        public static HttpException NotFound(this HttpResponse response, object error)
        {
            var disposable = error as IDisposable;

            if (disposable != null)
            {
                response.RegisterForDispose(disposable);
            }

            var result = new NotFoundObjectResult(error);
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
            => new HttpException(new ErrorResult());

        /// <summary>
        /// Creates an <see cref="HttpException" /> that produces an Error (500) response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="message">The error message to be returned to the client.</param>
        public static HttpException Error(this HttpResponse response, string message)
        {
            Requires.NotNullOrWhiteSpace(message, nameof(message));

            return response.Error(new ErrorMessage { Message = message });
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

            var result = new ErrorObjectResult(error);
            return new HttpException(result);
        }
    }
}