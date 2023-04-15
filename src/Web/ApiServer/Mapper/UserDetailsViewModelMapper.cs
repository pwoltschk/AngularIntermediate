using ApiServer.ViewModels;
using Application.Common.Services;

namespace ApiServer.Mapper;
public class UserDetailsViewModelMapper : IMapper<UserDetailsViewModel, User>
{
    public UserDetailsViewModel Map(User model)
    {
        return new UserDetailsViewModel
        {
            User = new UserDto
            {
                Email = model.Email,
                Id = model.Id,
                Name = model.Name
            },
            Roles = model.Roles.Select(Map).ToList(),
        };
    }

    private RoleDto Map(Role role)
    {
        var dto = new RoleDto
        {
            Id = role.Id,
            Name = role.Name
        };
        dto.Permissions.AddRange(role.Permissions);

        return dto;
    }

    public User Map(UserDetailsViewModel model)
    {
        throw new NotImplementedException();
    }
}
