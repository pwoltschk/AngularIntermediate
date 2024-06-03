using ApiServer.Mapper;
using ApiServer.ViewModels;
using Application.Roles.Commands;
using Application.Roles.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Identity;
using Permission = Shared.Identity.Permission;

namespace ApiServer.Controllers.Identity;

[Route("api/Identity/[controller]")]
public class RolesController(
    ISender mediator,
    IMapper<RolesViewModel, IEnumerable<Role>> mapper,
    IMapper<RoleDto, Role> roleMapper)
    : CustomControllerBase(mediator)
{
    [HttpGet]
    [Authorize(Permission.ReadRoles)]
    public async Task<ActionResult<RolesViewModel>> GetRoles()
    {
        return mapper.Map(await Mediator.Send(new GetRolesQuery()));
    }

    [HttpPost]
    [Authorize(Permission.WriteRoles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PostRole(RoleDto role)
    {
        await Mediator.Send(new CreateRoleCommand(roleMapper.Map(role)));

        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Permission.WriteRoles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutRole(RoleDto role)
    {
        await Mediator.Send(new UpdateRoleCommand(roleMapper.Map(role)));

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