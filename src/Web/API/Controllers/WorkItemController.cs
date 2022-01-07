using Application.WorkItems.Commands;
using Application.WorkItems.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostWorkItem(
            CreateWorkItemRequest request)
        {
            return await _mediator.Send(new CreateWorkItemCommand(request));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutWorkItem(int id,
            UpdateWorkItemRequest request)
        {
            if (id != request.Id) return BadRequest();

            await _mediator.Send(new UpdateWorkItemCommand(request));

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteWorkItem(int id)
        {
            await _mediator.Send(new DeleteWorkItemCommand(id));

            return NoContent();
        }
    }
}
