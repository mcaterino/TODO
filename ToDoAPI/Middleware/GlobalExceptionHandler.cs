using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using ToDoAPI.Models;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler>? _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler>? logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
      HttpContext httpContext, 
      Exception exception, 
      CancellationToken cancellationToken)
    {
        _logger?.LogError($"An error occurred while processing your request: {exception.Message}");


        var response = new ErrorResponse
        {
            Message = exception.Message,
        };

        switch (exception)
        {
            case BadHttpRequestException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Title = exception.GetType().Name;
                break;
            
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Title = "Internal Server Error";
                break;
        }

        httpContext.Response.StatusCode = response.StatusCode;

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}