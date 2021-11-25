using Clean.Architecture.Core.Application.DTO;
using MediatR;

namespace Clean.Architecture.Core.Application.Features.Message.Query.ById;

/// <summary>
/// Query to retrieve a Message with a given Id from the database
/// </summary>
public sealed class GetMessageByIdQuery : IRequest<MessageDto?>
{
    /// <summary>
    /// The Id of the Message to retrieve
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Constructor of the GetMessageByIdQuery class
    /// </summary>
    /// <param name="id">Value for the Id property</param>
    public GetMessageByIdQuery(int id)
    {
        Id = id;
    }
}