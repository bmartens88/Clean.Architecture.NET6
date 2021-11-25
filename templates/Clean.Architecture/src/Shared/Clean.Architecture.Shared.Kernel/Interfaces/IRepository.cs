using Ardalis.Specification;

namespace Clean.Architecture.Shared.Kernel.Interfaces;

    /// <summary>
    /// Interface for a repository
    /// </summary>
    /// <typeparam name="T">Entity type handled by this repository</typeparam>
public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot { }