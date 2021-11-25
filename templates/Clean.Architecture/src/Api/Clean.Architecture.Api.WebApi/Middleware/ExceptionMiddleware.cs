using Clean.Architecture.Api.WebApi.Errors;
using FluentValidation;
using System.Text.Json;

namespace Clean.Architecture.Api.WebApi.Middleware;

/// <summary>
/// Custom Exception handling middleware
/// </summary>
public sealed class ExceptionMiddleware
{
    // Reference to a delegate to the next middleware/handler in the request pipeline
    private readonly RequestDelegate _next;

    // Reference to ILogger instance for logging purposes
    private readonly ILogger<ExceptionMiddleware> _logger;

    // Reference to IHostEnvironment for environment information
    private readonly IHostEnvironment _env;

    /// <summary>
    /// Constructor of the ExceptionMiddleware class
    /// </summary>
    /// <param name="next"><see cref="RequestDelegate"/> to the next middleware/handler</param>
    /// <param name="logger"><see cref="ILogger{T}"/> instance for logging purposes</param>
    /// <param name="env"><see cref="IHostEnvironment"/> instance for environment information</param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    /// <summary>
    /// Invoked by the framework, which will in turn invoke the next delegate middleware/handler
    /// </summary>
    /// <param name="httpContext">Encapsulates all HTTP-specific information about an individual HTTP request</param>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            // Simply await the execution of the next delegate middleware/handler
            await _next(httpContext);
        }
        catch (ValidationException valEx)
        {
            _logger.LogError(valEx, $"Validation error thrown: {valEx.Message}");

            // When validation errors have occurred, extract the information from the exception and create proper response
            var errors = valEx.Errors.Select(x => x.ErrorMessage);
            var response = new ApiValidationErrorResponse { Errors = errors };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            // Serialize to JSON and return response
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
        catch (Exception ex)
        {
            // If an exception is thrown, handle accordingly
            _logger.LogError(ex, $"Exception was thrown: {ex.Message}");

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // Construct proper response
            var response = _env.IsDevelopment()
                ? new ApiException(StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace)
                : new ApiException(StatusCodes.Status500InternalServerError);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            // Serialize to JSON and return response
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }
}