using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.Api.WebApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    // IMediator instance
    private IMediator? _mediator;

    // IMediator instance for usage by derived classes
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
}