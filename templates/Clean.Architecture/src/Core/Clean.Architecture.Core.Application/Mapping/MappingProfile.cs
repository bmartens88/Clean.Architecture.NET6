using AutoMapper;
using System.Reflection;

namespace Clean.Architecture.Core.Application.Mapping;

/// <summary>
/// Profile for the application which creates all the mapping between types for AutoMapper
/// </summary>
public sealed class MappingProfile : Profile
{
    /// <summary>
    /// Constructor of the MappingProfile class
    /// </summary>
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Method which generates the mapping for AutoMapper
    /// This is done by iterating over all types implementing the <see cref="IMapFrom{T}"/> interface and
    /// invoking the Mapping method
    /// </summary>
    /// <param name="assembly">The assembly from which to extract all types implementing the <see cref="IMapFrom{T}"/> interface</param>
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        // Get all types from the provided assembly which implement the IMapFrom interface
        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in types)
        {
            // Create instance of type
            var instance = Activator.CreateInstance(type);

            // Get 'reference' to Mapping method
            var methodInfo = type.GetMethod("Mapping")
                             ?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping");

            // Invoke the Mapping method
            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}