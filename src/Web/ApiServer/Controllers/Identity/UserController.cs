using ApiServer.Mapper;
using ApiServer.ViewModels;
using Application.Roles.Queries;
using Application.Users.Command;
using Application.Users.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Identity;
using Permission = Shared.Identity.Permission;

namespace ApiServer.Controllers.Identity;

[Route("api/Admin/[controller]")]
public class UsersController(
    ISender mediator,
    IMapper<UserDto, User> userMapper,
    IMapper<UserDetailsViewModel, User> detailsMapper)
    : CustomControllerBase(mediator)
{
    [HttpGet]
    [Authorize(Permission.ReadUsers)]
    public async Task<ActionResult<UsersViewModel>> GetUsers()
    {
        return new UsersViewModel
        {
            Users = (await Mediator.Send(new GetUsersQuery())).Select(userMapper.Map).ToList()

        };
    }

    [HttpGet("{id}")]
    [Authorize(Permission.ReadUsers)]
    public async Task<ActionResult<UserDetailsViewModel>> GetUser(string id)
    {
        var detailsVm = detailsMapper.Map(await Mediator.Send(new GetUserQuery(id)));
        detailsVm.AllRoles = (await Mediator.Send(new GetRolesQuery())).Select(s => s.Name).ToList();
        return detailsVm;
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Permission.WriteUsers)]
    public async Task<IActionResult> PutUser(UserDto user)
    {
        await Mediator.Send(new UpdateUserCommand(userMapper.Map(user)));

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Permission.WriteUsers)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await Mediator.Send(new DeleteUserCommand(id));

        return Ok();
    }
}
