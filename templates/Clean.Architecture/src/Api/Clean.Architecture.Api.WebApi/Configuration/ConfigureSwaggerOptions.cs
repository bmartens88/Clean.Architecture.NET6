using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Clean.Architecture.Api.WebApi.Configuration;

/// <summary>
/// Class which configures the Swagger documentation generation option(s)
/// </summary>
public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    // Provider for versioned API descriptor
    private readonly IApiVersionDescriptionProvider _provider;

    /// <summary>
    /// Constructor of the ConfigureSwaggerOptions class
    /// </summary>
    /// <param name="provider"><see cref="IApiVersionDescriptionProvider"/> instance</param>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName,
                new OpenApiInfo
                {
                    Title = $"Sample API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString()
                });
    }
}