using MediatR;

namespace Clean.Architecture.Shared.Kernel.Events.Common;

/// <summary>
/// Base domain event from which all other domain events will derive
/// </summary>
public abstract class BaseDomainEvent : INotification
{
    /// <summary>
    /// The moment at which the event has occurred
    /// </summary>
    public DateTime DateOccurred { get; set; } = DateTime.UtcNow;
}