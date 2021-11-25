using Ardalis.Specification.EntityFrameworkCore;
using Clean.Architecture.Shared.Kernel.Interfaces;

namespace Clean.Architecture.Infrastructure.Persistence.Data;

/// <summary>
/// Implementation of the generic repository pattern in this application
/// </summary>
/// <typeparam name="T">Entity type for which this repository is used</typeparam>
public sealed class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(AppDbContext context)
        : base(context) { }
}