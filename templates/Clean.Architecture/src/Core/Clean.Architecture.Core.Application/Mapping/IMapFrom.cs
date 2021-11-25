using AutoMapper;

namespace Clean.Architecture.Core.Application.Mapping;

/// <summary>
/// Interface which enables automatic mapping from type <typeparamref name="T"/> to the current type
/// </summary>
/// <typeparam name="T">The type from which to map from</typeparam>
public interface IMapFrom<T>
{
    /// <summary>
    /// Method which generates the map for AutoMapper
    /// </summary>
    /// <param name="profile">Provides a named configuration for maps</param>
    void Mapping(Profile profile) =>
        profile.CreateMap(typeof(T), GetType());
}