using Clean.Architecture.Core.Domain.Entities;
using Clean.Architecture.Shared.Kernel.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Clean.Architecture.Infrastructure.Persistence.Data;

/// <summary>
/// DbContext instance for this application
/// </summary>
public sealed class AppDbContext : DbContext
{
    private readonly IMediator? _mediator;

    /// <summary>
    /// Constructor of the EfContext class
    /// </summary>
    /// <param name="options"><see cref="DbContextOptions{TContext}"/> instance with configuration</param>
    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator? mediator)
        : base(options)
    {
        _mediator = mediator;
    }

    // Messages table accessor
    public DbSet<Message> Messages => Set<Message>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Update properties for each record accordingly
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.LastModified = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    entry.Entity.LastModified = DateTime.UtcNow;
                    entry.Entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                    break;
            }

        int result = await base.SaveChangesAsync(cancellationToken);

        // Ignore events if no dispatcher is provided
        if (_mediator == null) return result;

        // Dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.Events.Any())
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.Events.ToArray();
            entity.Events.Clear();
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }

        return result;
    }

    /// <inheritdoc />
    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}