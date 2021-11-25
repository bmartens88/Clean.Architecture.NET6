using FluentValidation;
using MediatR;

namespace Clean.Architecture.Core.Application.Behavior;

/// <summary>
/// MediatR behavior implementation for validation of incoming request(s)
/// </summary>
/// <typeparam name="TRequest">Type of the request sent by MediatR</typeparam>
/// <typeparam name="TResponse">Type of the response expected from the sent request</typeparam>
public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    // Reference to (Fluent) validators
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Constructor of the ValidationBehavior class
    /// </summary>
    /// <param name="validators"><see cref="IEnumerable{T}"/> containing validators for the current request</param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Pipeline handler. Perform any additional behavior and await the <paramref name="next"/> delegate as necessary
    /// </summary>
    /// <param name="request">Incoming request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler</param>
    /// <returns>Awaitable task returning the <typeparamref name="TResponse"/></returns>
    /// <exception cref="ValidationException">Thrown when one or more validation(s) fail</exception>
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        // When no validators are found for the request, await the delegate
        if (!_validators.Any()) return await next();

        // Create validation context
        var context = new ValidationContext<TRequest>(request);

        // Collect validation results and the errors from those results
        var validationResults =
            await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        // When there are errors, throw exception
        if (failures.Count != 0)
            throw new ValidationException(failures);

        // Await delegate for the next handler
        return await next();
    }
}