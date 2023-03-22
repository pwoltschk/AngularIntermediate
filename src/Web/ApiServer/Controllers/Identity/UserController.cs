using ApiServer.Identity;
using ApiServer.Mapper;
using ApiServer.ViewModels;
using Application.Common.Services;
using Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers.Identity;

[Route("api/Admin/[controller]")]
public class UsersController : CustomControllerBase
{
    private readonly IMapper<UsersViewModel, IEnumerable<User>> _mapper;
    private readonly IMapper<UserDetailsViewModel, User> _detailsMapper;

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
}
