using Clean.Architecture.Core.Application.DTO;
using Clean.Architecture.Core.Application.Features.Message.Query.All;
using Clean.Architecture.Core.Application.Features.Message.Query.ById;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.Api.WebApi.Controllers.v2;

/// <summary>
/// Sample controller for demonstrating versioning, MediatR and UnitOfWork etc
/// </summary>
[ApiVersion("2.0")]
public sealed class MessageController : BaseApiController
{
    /// <summary>
    /// Get all messages stored in the in-memory database
    /// </summary>
    /// <returns><see cref="IReadOnlyCollection{T}"/> with Messages</returns>
    /// <response code="200">Returns all of the messages in the database</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<MessageDto>>> GetMessages()
    {
        return Ok(await Mediator.Send(new GetAllMessagesQuery()));
    }

    /// <summary>
    /// Retrieve a Message by Id
    /// </summary>
    /// <param name="id">Id of the message to retrieve</param>
    /// <returns><see cref="MessageDto"/> of the Message with the given Id</returns>
    /// <response code="200">Returns the message with the given Id</response>
    /// <response code="404">When no message with the given Id is found</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MessageDto>> GetMessageById(int id)
    {
        var message = await Mediator.Send(new GetMessageByIdQuery(id));
        return message == null ? NotFound() : Ok(message);
    }
}