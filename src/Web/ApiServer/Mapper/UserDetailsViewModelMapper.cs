using ApiServer.ViewModels;
using Domain.Entities;

namespace ApiServer.Mapper;
public class UserDetailsViewModelMapper(
    IMapper<RoleDto, Role> roleMapper,
    IMapper<UserDto, User> userMapper)
    : IMapper<UserDetailsViewModel, User>
{
    public UserDetailsViewModel Map(User model)
    {
        return new UserDetailsViewModel
        {
            User = userMapper.Map(model),
            Roles = model.Roles.Select(roleMapper.Map).ToList()
        };
    }
    public User Map(UserDetailsViewModel model)
    {
        throw new NotImplementedException();
    }
}
