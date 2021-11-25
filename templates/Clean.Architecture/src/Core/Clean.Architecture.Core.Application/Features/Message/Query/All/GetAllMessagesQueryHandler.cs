using AutoMapper;
using Clean.Architecture.Core.Application.DTO;
using Clean.Architecture.Shared.Kernel.Interfaces;
using MediatR;

namespace Clean.Architecture.Core.Application.Features.Message.Query.All;

/// <summary>
/// Handler class for requests of type <see cref="GetAllMessagesQuery"/>
/// </summary>
public sealed class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, IReadOnlyCollection<MessageDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadRepository<Domain.Entities.Message> _repository;

    /// <summary>
    /// Constructor of the GetAllMessagesQueryHandler class
    /// </summary>
    /// <param name="mapper"><see cref="IMapper"/> instance from AutoMapper</param>
    /// <param name="repository">Read-only repository for the requested type</param>
    public GetAllMessagesQueryHandler(IMapper mapper, IReadRepository<Domain.Entities.Message> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<MessageDto>> Handle(GetAllMessagesQuery _, CancellationToken cancellationToken)
    {
        var messages = await _repository.ListAsync(cancellationToken);
        return _mapper.Map<IReadOnlyCollection<MessageDto>>(messages);
    }
}