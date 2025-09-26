using System.Net;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace IoTHub.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
     private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }
    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        HttpStatusCode status = HttpStatusCode.InternalServerError;
        string errorMessage = exception.Message;
        switch (exception)
        {
            case UnauthorizedAccessException:
                status = HttpStatusCode.Unauthorized;
                break;
            case BadHttpRequestException:
            case ArgumentNullException:
            case ArgumentException:
                status = HttpStatusCode.BadRequest;
                break;
            /*case NotFoundException:
                status = HttpStatusCode.NoContent;
                break;
            case ForbiddenException:
                status = HttpStatusCode.Forbidden;
                break;*/
            case ValidationException:
                status = HttpStatusCode.BadRequest;
                break;
            case DbUpdateConcurrencyException:
                status = HttpStatusCode.Conflict;
                break;
        }
        
        httpContext.Response.StatusCode = (int)status;
        var errorResponse = new
        {
            Success = false,
            Message = errorMessage,
            StatusCode = httpContext.Response.StatusCode,
            ErrorDetails = exception.StackTrace,
            Detail = exception.Message
        };
        _logger.LogError(exception, errorResponse.Message);
        await httpContext.Response.WriteAsJsonAsync(errorResponse);
    }
}