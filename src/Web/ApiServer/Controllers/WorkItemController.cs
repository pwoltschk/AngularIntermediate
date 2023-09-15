using ApiServer.Mapper;
using ApiServer.ViewModels;
using Application.Projects.Queries;
using Application.WorkItems.Commands;
using Application.WorkItems.Requests;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Identity;
using Permission = Shared.Identity.Permission;

namespace ApiServer.Controllers
{
    public class WorkItemController : CustomControllerBase
    {
        private readonly IMapper<WorkItemsView, IEnumerable<WorkItem>> _mapper;

        public WorkItemController(IMediator mediator,
            IMapper<WorkItemsView, IEnumerable<WorkItem>> mapper) : base(mediator)
        {
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize(Permission.ReadProjects)]
        public async Task<ActionResult<WorkItemsView>> GetWorkItems()
        {
            return _mapper.Map(await Mediator.Send(new GetWorkItemsQuery()));
        }

        [HttpPost]
        [Authorize(Permission.WriteProjects)]
        public async Task<ActionResult<int>> PostWorkItem(
            CreateWorkItemRequest request)
        {
            return await Mediator.Send(new CreateWorkItemCommand(request));
        }

        [HttpPut("{id}")]
        [Authorize(Permission.WriteProjects)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutWorkItem(int id,
            UpdateWorkItemRequest request)
        {
            if (id != request.Id) return BadRequest();

            await Mediator.Send(new UpdateWorkItemCommand(request));

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Permission.WriteProjects)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteWorkItem(int id)
        {
            await Mediator.Send(new DeleteWorkItemCommand(id));

            return Ok();
        }
    }
}
