using Clean.Architecture.Shared.Kernel.Events.Common;

namespace Clean.Architecture.Shared.Kernel.Entities.Common;

/// <summary>
/// Abstract base entity from which all other entities will derive
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// The (unique) id of the entity
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// When the record was created
    /// </summary>
    public DateTime Created { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// When the record was last modified (if ever)
    /// </summary>
    public DateTime? LastModified { get; set; }

    /// <summary>
    /// Property used for soft-deleting entities
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Collection of events for this entity
    /// </summary>
    public List<BaseDomainEvent> Events = new();
}