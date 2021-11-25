using Clean.Architecture.Shared.Kernel.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Architecture.Infrastructure.Persistence.Data.Config.Common;

/// <summary>
/// Base entity configuration which all other configuration should inherit from
/// </summary>
/// <typeparam name="TEntity">The entity for which the configuration is being performed</typeparam>
public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    /// <inheritdoc />
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasQueryFilter(b => !b.IsDeleted);
    }
}