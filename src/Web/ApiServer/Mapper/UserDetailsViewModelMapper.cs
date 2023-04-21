using ApiServer.ViewModels;
using Application.Common.Services;

namespace ApiServer.Mapper;
public class UserDetailsViewModelMapper : IMapper<UserDetailsViewModel, User>
{
    private readonly IMapper<RoleDto, Role> _roleMapper;
    private readonly IMapper<UserDto, User> _userMapper;

    public UserDetailsViewModelMapper(
        IMapper<RoleDto, Role> roleMapper,
        IMapper<UserDto, User> userMapper)
    {
        _roleMapper = roleMapper;
        _userMapper = userMapper;
    }

    public UserDetailsViewModel Map(User model)
    {
        return new UserDetailsViewModel
        {
            User = _userMapper.Map(model),
            Roles = model.Roles.Select(_roleMapper.Map).ToList(),
        };
    }
    public User Map(UserDetailsViewModel model)
    {
        throw new NotImplementedException();
    }
}
