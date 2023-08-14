using EntityFramework.Exceptions.Common;
using Facets.SharedKernal.Helpers;
using Facets.SharedKernal.Responses;
using Microsoft.ApplicationInsights;
using Serilog;
using System.Diagnostics;
using System.Net;

namespace Facets.Api.Middleware;

public sealed class ExceptionHandlerMiddleware
{
    private const string applicationJSONContentType = "application/json";
    private readonly RequestDelegate _next;
    private readonly TelemetryClient _telemetry;

    public ExceptionHandlerMiddleware(RequestDelegate next, TelemetryClient telemetry)
    {
        _next = next;
        _telemetry = telemetry;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await ConvertException(context, ex);
        }
    }

    private Task ConvertException(HttpContext context, Exception exception)
    {
        var activityId = Activity.Current?.Id ?? "N/A";

        _telemetry.TrackException(exception);

        ErrorResponse errorResponse = new() { TraceId = activityId };

        int httpStatusCode = StatusCodes.Status500InternalServerError;

        context.Response.ContentType = applicationJSONContentType;

        var result = string.Empty;



        switch (exception)
        {
            case UniqueConstraintException:
                httpStatusCode = StatusCodes.Status400BadRequest;
                errorResponse.Errors.Add(new KeyValuePair<string, IEnumerable<string>>("Duplicate", new[] { "Duplicate value detected" }));
                result = Serializer.Serialize(errorResponse);
                LogError(exception, activityId);
                break;

            case OperationCanceledException:
                //if client closes the connection
                httpStatusCode = StatusCodes.Status200OK;
                result = Serializer.Serialize(new ResponseResult<string>("Client closed the connecion"));
                break;
            case Exception:
                httpStatusCode = StatusCodes.Status500InternalServerError;
                errorResponse.Errors.Add(new KeyValuePair<string, IEnumerable<string>>(nameof(HttpStatusCode.InternalServerError), new[] { "Something went wrong, please try again" }));
                result = Serializer.Serialize(errorResponse);
                LogError(exception, activityId);
                break;
        }

        context.Response.StatusCode = httpStatusCode;

        return context.Response.WriteAsync(result);
    }

    private static void LogError(Exception exception, string activityId)
    {
        Log.Error("\n{startLine} \n\n Type:\n{exceptionType} \n\n ActivityId:\n{activity}\n\n Message:\n{exceptionMessage} \n\n Stack Trace:\n                              {stackTrace} \n{endLine}\n",
                                new string('-', 150),
                                exception.GetType().FullName,
                                activityId,
                                exception?.InnerException?.Message ?? exception?.Message,
                                exception?.InnerException?.StackTrace ?? exception?.StackTrace,
                                new string('-', 150));
    }
}
