using ApiServer.Mapper;
using ApiServer.ViewModels;
using Application.Common.Services;
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
public class UsersController : CustomControllerBase
{
    private readonly IMapper<UserDetailsViewModel, User> _detailsMapper;
    private readonly IMapper<UserDto, User> _userMapper;

    public UsersController(
        ISender mediator,
        IMapper<UserDto, User> userMapper,
        IMapper<UserDetailsViewModel, User> detailsMapper) : base(mediator)
    {
        _userMapper = userMapper;
        _detailsMapper = detailsMapper;
    }

    [HttpGet]
    [Authorize(Shared.Identity.Permission.ReadUsers)]
    public async Task<ActionResult<UsersViewModel>> GetUsers()
    {
        return new UsersViewModel
        {
            Users = (await Mediator.Send(new GetUsersQuery())).Select(_userMapper.Map).ToList(),

        };
    }

    [HttpGet("{id}")]
    [Authorize(Permission.ReadUsers)]
    public async Task<ActionResult<UserDetailsViewModel>> GetUser(string id)
    {
        var detailsVm = _detailsMapper.Map(await Mediator.Send(new GetUserQuery(id)));
        detailsVm.AllRoles = (await Mediator.Send(new GetRolesQuery())).Select(s => s.Name).ToList();
        return detailsVm;
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Permission.WriteUsers)]
    public async Task<IActionResult> PutUser(UserDto user)
    {
        await Mediator.Send(new UpdateUserCommand(_userMapper.Map(user)));

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
