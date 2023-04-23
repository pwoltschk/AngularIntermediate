using ApiServer.Identity;
using ApiServer.Mapper;
using ApiServer.ViewModels;
using Application.Common.Services;
using Application.Users.Command;
using Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers.Identity;

[Route("api/Admin/[controller]")]
public class UsersController : CustomControllerBase
{
    private readonly IMapper<UsersViewModel, IEnumerable<User>> _mapper;
    private readonly IMapper<UserDetailsViewModel, User> _detailsMapper;
    private readonly IMapper<UserDto, User> _userMapper;

    public UsersController(
        ISender mediator,
        IMapper<UsersViewModel, IEnumerable<User>> mapper,
        IMapper<UserDetailsViewModel, User> detailsMapper) : base(mediator)
    {
        _mapper = mapper;
        _detailsMapper = detailsMapper;
    }

    [HttpGet]
    [Authorize(Permission.ReadUsers)]
    public async Task<ActionResult<UsersViewModel>> GetUsers()
    {
        return _mapper.Map(await Mediator.Send(new GetUsersQuery()));
    }

    [HttpGet("{id}")]
    [Authorize(Permission.ReadUsers)]
    public async Task<ActionResult<UserDetailsViewModel>> GetUser(string id)
    {
        return _detailsMapper.Map(await Mediator.Send(new GetUserQuery(id)));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Permission.WriteUsers)]
    public async Task<IActionResult> PutUser(UserDto user)
    {
        await Mediator.Send(new UpdateUserCommand(_userMapper.Map(user)));

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Permission.WriteUsers)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await Mediator.Send(new DeleteUserCommand(id));

        return Ok();
    }
}
