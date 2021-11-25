using Ardalis.Specification;

namespace Clean.Architecture.Core.Application.Specifications.Message;

/// <summary>
/// Specification to retrieve a Message by its Id
/// </summary>
public sealed class MessageByIdSpecification : Specification<Domain.Entities.Message>, ISingleResultSpecification
{
    /// <summary>
    /// Constructor of the MessageByIdSpecification class
    /// </summary>
    /// <param name="id">The Id of the Message to retrieve</param>
    public MessageByIdSpecification(int id)
    {
        Query
            .Where(m => m.Id == id);
    }
}