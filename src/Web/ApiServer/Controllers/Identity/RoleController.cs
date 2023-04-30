using ApiServer.Identity;
using ApiServer.Mapper;
using ApiServer.ViewModels;
using Application.Common.Services;
using Application.Roles.Commands;
using Application.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers.Identity;

[Route("api/Identity/[controller]")]
public class RolesController : CustomControllerBase
{
    private readonly IMapper<RolesViewModel, IEnumerable<Role>> _vmMapper;
    private readonly IMapper<RoleDto, Role> _roleMapper;

    public RolesController(
        ISender mediator,
        IMapper<RolesViewModel, IEnumerable<Role>> mapper,
        IMapper<RoleDto, Role> roleMapper)
        : base(mediator)
    {
        _vmMapper = mapper;
        _roleMapper = roleMapper;
    }

    [HttpGet]
    [Authorize(Permission.ReadRoles)]
    public async Task<ActionResult<RolesViewModel>> GetRoles()
    {
        return _vmMapper.Map(await Mediator.Send(new GetRolesQuery()));
    }

    [HttpPost]
    [Authorize(Permission.WriteRoles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PostRole(RoleDto role)
    {
        await Mediator.Send(new CreateRoleCommand(_roleMapper.Map(role)));

        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Permission.WriteRoles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutRole(RoleDto role)
    {
        await Mediator.Send(new UpdateRoleCommand(_roleMapper.Map(role)));

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Permission.WriteRoles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRole(string id)
    {
        await Mediator.Send(new DeleteRoleCommand(id));

        return Ok();
    }
}