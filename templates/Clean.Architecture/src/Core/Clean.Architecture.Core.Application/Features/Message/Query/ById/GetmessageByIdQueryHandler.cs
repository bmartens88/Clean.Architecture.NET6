using AutoMapper;
using Clean.Architecture.Core.Application.DTO;
using Clean.Architecture.Core.Application.Specifications.Message;
using Clean.Architecture.Shared.Kernel.Interfaces;
using MediatR;

namespace Clean.Architecture.Core.Application.Features.Message.Query.ById;

/// <summary>
/// Handler class for requests of type <see cref="GetMessageByIdQuery"/>
/// </summary>
public sealed class GetmessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, MessageDto?>
{
    private readonly IMapper _mapper;
    private readonly IReadRepository<Domain.Entities.Message> _repository;

    /// <summary>
    /// Constructor of the GetMessageByIdQueryHandler class
    /// </summary>
    /// <param name="mapper"><see cref="IMapper"/> instance from AutoMapper</param>
    /// <param name="repository">Read-only repository for the requested type</param>
    public GetmessageByIdQueryHandler(IMapper mapper, IReadRepository<Domain.Entities.Message> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<MessageDto?> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
    {
        // Create specification
        var spec = new MessageByIdSpecification(request.Id);
        // Retrieve message from database (if any)
        var message = await _repository.GetBySpecAsync(spec, cancellationToken);
        return message != null ? _mapper.Map<MessageDto>(message) : null;
    }
}