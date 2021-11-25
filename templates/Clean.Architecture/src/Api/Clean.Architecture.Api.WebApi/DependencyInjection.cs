using Clean.Architecture.Api.WebApi.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Clean.Architecture.Api.WebApi;

/// <summary>
/// Class used for Dependency Injection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the services from the application Assembly to the DI container
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> used for registration of service(s)</param>
    /// <param name="configuration"><see cref="IConfiguration"/> instance for further configuration</param>
    /// <returns><see cref="IServiceCollection"/> with services registered</returns>
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add controller support
        services.AddControllers();

        // Add support for API versioning
        services.AddApiVersioning(opts =>
        {
            opts.DefaultApiVersion = new ApiVersion(1, 0);
            opts.AssumeDefaultVersionWhenUnspecified = true;
            opts.ReportApiVersions = true;
        });

        // Add support for API versioning explorer (Swagger)
        services.AddVersionedApiExplorer(opts =>
        {
            opts.GroupNameFormat = "'v'VVV";
            opts.SubstituteApiVersionInUrl = true;
        });

        // Configure and add Swagger
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}