using ApiServer.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers
{
    [ApiController]
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
}
