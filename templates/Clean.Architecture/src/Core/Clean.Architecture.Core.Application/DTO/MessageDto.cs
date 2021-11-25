using Clean.Architecture.Core.Application.Mapping;
using Clean.Architecture.Core.Domain.Entities;

namespace Clean.Architecture.Core.Application.DTO;

/// <summary>
/// Data-Transfer-Object for the <see cref="Message"/> entity
/// </summary>
public sealed class MessageDto : IMapFrom<Message>
{
    /// <summary>
    /// The message text, as stored in the database
    /// </summary>
    public string MessageText { get; set; } = string.Empty;
}