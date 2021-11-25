using Clean.Architecture.Shared.Kernel.Entities.Common;
using Clean.Architecture.Shared.Kernel.Interfaces;

namespace Clean.Architecture.Core.Domain.Entities;

/// <summary>
/// Example entity for usage with the template's in-memory database
/// </summary>
public sealed class Message : BaseEntity, IAggregateRoot
{
    /// <summary>
    /// Text of the message, as stored in the database
    /// </summary>
    public string MessageText { get; set; } = string.Empty;
}