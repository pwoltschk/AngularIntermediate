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
    protected CustomControllerBase(ISender mediator)
    {
        Mediator = mediator;
    }

    protected ISender Mediator { get; }
}