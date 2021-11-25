using Clean.Architecture.Core.Application.Features.Hello.Query;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.Api.WebApi.Controllers.v1;

/// <summary>
/// Sample controller for demonstrating versioning an using MediatR
/// </summary>
[ApiVersion("1.0")]
public sealed class HelloWorldController : BaseApiController
{
    /// <summary>
    /// Get a message from the system
    /// </summary>
    /// <returns>A message from the system</returns>
    /// <response code="200">Returns a message from the system</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public ActionResult<string> Get()
    {
        return Ok("Hello world!");
    }

    /// <summary>
    /// Get a greeting from the system
    /// </summary>
    /// <param name="query"><see cref="GetGreetingQuery"/> instance passed via the request body</param>
    /// <returns>A greeting from the system</returns>
    /// <response code="200">Returns a greeting from the system</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<ActionResult<string>> GetGreeting(GetGreetingQuery query)
    {
        return await Mediator.Send(query);
    }

    /// <summary>
    /// Dummy controller method to trigger an exception
    /// </summary>
    /// <returns>API response containing exception info</returns>
    /// <exception cref="Exception">Default exception thrown for demonstration purposes</exception>
    /// <response code="500">Internal server error, as an exception is being thrown</response>
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("[action]")]
    public ActionResult GetException()
    {
        throw new Exception();
    }
}