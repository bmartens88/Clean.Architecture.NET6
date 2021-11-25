namespace Clean.Architecture.Api.WebApi.Errors;

/// <summary>
/// API exception class
/// </summary>
public sealed class ApiException : ApiResponse
{
    /// <summary>
    /// Constructor of the ApiException class
    /// </summary>
    /// <param name="statusCode">The status code of the response</param>
    /// <param name="message">The optional message of the response</param>
    /// <param name="details">Optional details of the exception. Primarily used during development</param>
    public ApiException(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
    {
        Details = details;
    }

    /// <summary>
    /// Additional details of the cause for the exception. Primarily used during development
    /// </summary>
    public string? Details { get; private set; }
}