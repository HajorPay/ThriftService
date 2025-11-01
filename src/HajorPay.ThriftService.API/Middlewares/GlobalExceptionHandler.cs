using HajorPay.ThriftService.API.Common.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HajorPay.ThriftService.API.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IHostEnvironment _env;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";
        private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
        {
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        public GlobalExceptionHandler(IHostEnvironment env, ILogger<GlobalExceptionHandler> logger)
        {
            _env = env;
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is null)
            {
                exception = new Exception("An unknown error occurred."); // Fallback exception
            }

            exception.AddErrorCode();

            //If your logger logs "Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware", you should remove the string below to avoid the exception being logged twice.
            _logger.LogError(exception, exception?.Message ?? UnhandledExceptionMsg);

            var problemDetails = CreateProblemDetails(httpContext, exception);
            var json = ToJson(problemDetails);

            httpContext.Response.ContentType = "application/problem+json";
            await httpContext.Response.WriteAsync(json, cancellationToken);

            return true;
        }

        private ProblemDetails CreateProblemDetails(in HttpContext context, in Exception exception)
        {
            var errorCode = exception.GetErrorCode();
            var statusCode = context.Response.StatusCode;
            var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);
            if (string.IsNullOrEmpty(reasonPhrase))
            {
                reasonPhrase = UnhandledExceptionMsg;
            }

            var problemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Status = statusCode,
                Title = reasonPhrase,
                Extensions =
                {
                    [nameof(errorCode)] = errorCode,
                    ["errors"] = exception.GetValidationErrors()
                }
            };

            if (!_env.IsDevelopment())
            {
                return problemDetails;
            }

            problemDetails.Detail = exception.ToString();
            problemDetails.Extensions["traceId"] = Activity.Current?.Id;
            problemDetails.Extensions["requestId"] = context.TraceIdentifier;
            problemDetails.Extensions["data"] = exception.Data;

            return problemDetails;
        }

        private string ToJson(in ProblemDetails problemDetails)
        {
            try
            {
                return JsonSerializer.Serialize(problemDetails, SerializerOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception has occurred while serializing error to JSON");
            }

            return string.Empty;
        }


        //public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        //{
        //    _logger.LogError(exception, exception.Message);

        //    var problemDetails = new ProblemDetails()
        //    {
        //        Detail = $"API Error {exception.Message}",
        //        Instance = "API",
        //        Status = (int)HttpStatusCode.InternalServerError,
        //        Title = "API Error",
        //        Type = "Server Error"
        //    };

        //    var response = JsonSerializer.Serialize(problemDetails);

        //    httpContext.Response.ContentType = "application/json";

        //    await httpContext.Response.WriteAsync(response, cancellationToken);

        //    return true;
        ////TODO: check null checks in validators
        ////how to throow exception in createuser/registeruser
        //}
    }
}
