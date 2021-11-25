using Clean.Architecture.Api.WebApi.Middleware;

namespace Clean.Architecture.Api.WebApi.Extensions;

/// <summary>
/// Class containing extension methods on the ApplicationBuilder class
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configures usage of the <see cref="ExceptionMiddleware"/> middleware class
    /// </summary>
    /// <param name="builder"><see cref="IApplicationBuilder"/> instance to configure</param>
    /// <returns><see cref="IApplicationBuilder"/> with the <see cref="ExceptionMiddleware"/> middleware configured</returns>
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<ExceptionMiddleware>();
}