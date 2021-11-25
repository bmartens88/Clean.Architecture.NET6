using Clean.Architecture.Core.Application.Behavior;
using Clean.Architecture.Core.Application.Mapping;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Architecture.Core.Application;

/// <summary>
/// Class used for registering services for dependency injection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the services from the application Assembly to the DI container
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> used for registration of service(s)</param>
    /// <returns><see cref="IServiceCollection"/> with services registered</returns>
    public static IServiceCollection AddApplicationCore(this IServiceCollection services) =>
        services
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddAutoMapper(typeof(MappingProfile).Assembly)
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}