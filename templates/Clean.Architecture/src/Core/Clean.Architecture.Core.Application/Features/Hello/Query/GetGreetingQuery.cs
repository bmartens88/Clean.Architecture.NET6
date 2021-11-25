using MediatR;

namespace Clean.Architecture.Core.Application.Features.Hello.Query;

/// <summary>
/// A simple query which will return a message from the system
/// </summary>
public sealed class GetGreetingQuery : IRequest<string>
{
    /// <summary>
    /// Name property for usage inside of the message
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Constructor of the GetGreetingQuery class
    /// </summary>
    /// <param name="name">Value for the Name property</param>
    public GetGreetingQuery(string name)
    {
        Name = name;
    }
}