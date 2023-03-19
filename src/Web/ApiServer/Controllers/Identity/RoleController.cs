using ApiServer.Identity;
using ApiServer.Mapper;
using ApiServer.ViewModels;
using Application.Common.Services;
using Application.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers.Identity;

[Route("api/Identity/[controller]")]
public class RolesController : CustomControllerBase
{
    private readonly IMapper<RolesViewModel, IEnumerable<Role>> _mapper;

    public RolesController(ISender mediator, IMapper<RolesViewModel, IEnumerable<Role>> mapper) : base(mediator)
    {
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Permission.ReadRoles)]
    public async Task<ActionResult<RolesViewModel>> GetRoles()
    {
        return _mapper.Map(await Mediator.Send(new GetRolesQuery()));
    }
}