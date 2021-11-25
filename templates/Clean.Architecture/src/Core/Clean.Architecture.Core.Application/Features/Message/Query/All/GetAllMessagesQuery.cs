using Clean.Architecture.Core.Application.DTO;
using MediatR;

namespace Clean.Architecture.Core.Application.Features.Message.Query.All;

/// <summary>
/// Query to retrieve all messages stored in the database
/// </summary>
public sealed class GetAllMessagesQuery : IRequest<IReadOnlyCollection<MessageDto>> { }