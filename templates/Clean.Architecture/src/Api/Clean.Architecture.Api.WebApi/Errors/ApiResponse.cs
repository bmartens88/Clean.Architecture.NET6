namespace Clean.Architecture.Api.WebApi.Errors;

/// <summary>
/// Base API response class to generate a consistent response 'model'
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// Constructor of the ApiResponse class
    /// </summary>
    /// <param name="statusCode">Status code of the response</param>
    /// <param name="message">Optional message to provide to the response</param>
    public ApiResponse(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    /// <summary>
    /// Status code of this response
    /// </summary>
    public int StatusCode { get; private set; }

    /// <summary>
    /// The message of this response
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// Helper method which generates default response message(s) based on status code
    /// </summary>
    /// <param name="statusCode">Status code of the response</param>
    /// <returns>Default message for the response</returns>
    private static string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "A bad request was made",
            401 => "User is not authorized",
            404 => "The requested resource was not found",
            500 => "Internal server error occurred",
            _ => throw new Exception(message: "Unhandled status code received")
        };
    }
}