namespace Clean.Architecture.Api.WebApi.Errors;

/// <summary>
/// API validation error class
/// </summary>
public class ApiValidationErrorResponse : ApiResponse
{
    /// <summary>
    /// Constructor of the ApiValidationErrorResponse class
    /// </summary>
    public ApiValidationErrorResponse() : base(400)
    {
    }

    /// <summary>
    /// The validation errors which caused this response to occur
    /// </summary>
    public IEnumerable<string>? Errors { get; init; }
}