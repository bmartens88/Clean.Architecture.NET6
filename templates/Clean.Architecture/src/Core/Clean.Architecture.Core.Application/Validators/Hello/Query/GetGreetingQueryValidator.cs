using Clean.Architecture.Core.Application.Features.Hello.Query;
using FluentValidation;

namespace Clean.Architecture.Core.Application.Validators.Hello.Query;

/// <summary>
/// Validator for the <see cref="GetGreetingQuery"/> class
/// </summary>
public sealed class GetGreetingQueryValidator : AbstractValidator<GetGreetingQuery>
{
    /// <summary>
    /// Constructor of the GetGreetingQueryValidator class
    /// </summary>
    public GetGreetingQueryValidator()
    {
        // The Name property should not be null, empty or whitespace
        RuleFor(q => q.Name)
            .NotEmpty();
    }
}