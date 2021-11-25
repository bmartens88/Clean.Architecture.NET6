using MediatR;

namespace Clean.Architecture.Core.Application.Features.Hello.Query;

/// <summary>
/// Handler class for requests of type <see cref="GetGreetingQuery"/>
/// </summary>
public sealed class GetGreetingQueryHandler : IRequestHandler<GetGreetingQuery, string>
{
    /// <inheritdoc />
    public Task<string> Handle(GetGreetingQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult($"Hello, {request.Name}");
    }
}