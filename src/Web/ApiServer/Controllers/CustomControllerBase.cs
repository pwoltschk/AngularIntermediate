using ApiServer.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers;

[ApiController]
[Authorize]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class CustomControllerBase : ControllerBase
{
    private readonly ISender _mediator;

    public CustomControllerBase(ISender mediator)
    {
        _mediator = mediator;
    }

    protected ISender Mediator => _mediator;
}