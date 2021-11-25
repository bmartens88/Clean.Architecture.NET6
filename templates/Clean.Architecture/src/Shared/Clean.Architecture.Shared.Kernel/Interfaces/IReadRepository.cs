using Ardalis.Specification;

namespace Clean.Architecture.Shared.Kernel.Interfaces;

/// <summary>
/// Interface for read-only repository
/// </summary>
/// <typeparam name="T">Entity type handled by this repository</typeparam>
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot { }