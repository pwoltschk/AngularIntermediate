using ApiServer.ViewModels;
using Domain.Entities;
using Domain.ValueObjects;

namespace ApiServer.Mapper;

public class UserMapper : IMapper<UserDto, User>
{
    public UserDto Map(User model)
    {
        return new UserDto
        {
            Email = model.Email.Value,
            Id = model.Id,
            Name = model.Name,
            Roles = model.Roles.Select(s => s.Name)
        };
    }

    public User Map(UserDto model)
    {
        var email = new Email(model.Email);
        var user = new User(model.Id, model.Name, email);
        user.Roles.AddRange(model.Roles.Select(r => new Role(string.Empty, r, new List<string>())));
        return user;
    }
}
