using ApiServer.Mapper;
using ApiServer.ViewModels;
using Application.Projects.Commands;
using Application.Projects.Queries;
using Application.Projects.Requests;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers
{
    public class ProjectController : CustomControllerBase
    {
        private readonly IMapper<ProjectsViewModel, IEnumerable<Project>> _mapper;

        public ProjectController(
            IMediator mediator,
            IMapper<ProjectsViewModel, IEnumerable<Project>> mapper) : base(mediator)
        {
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<int>> PostProject(
            CreateProjectRequest request)
        {
            return await Mediator.Send(new CreateProjectCommand(request));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutProject(int id,
            UpdateProjectRequest request)
        {
            if (id != request.Id) return BadRequest();

            await Mediator.Send(new UpdateProjectCommand(request));

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await Mediator.Send(new DeleteProjectCommand(id));

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<ProjectsViewModel>> GetProjects()
        {
            return _mapper.Map(await Mediator.Send(new GetProjectsQuery()));
        }
    }
}
