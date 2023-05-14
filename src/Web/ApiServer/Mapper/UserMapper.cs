using ApiServer.ViewModels;
using Application.Common.Services;

namespace ApiServer.Mapper
{
    public class UserMapper : IMapper<UserDto, User>
    {
        public UserDto Map(User model)
        {
            return new UserDto
            {
                Email = model.Email,
                Id = model.Id,
                Name = model.Name,
                Roles = model.Roles.Select(s => s.Name)
            };
        }

        public User Map(UserDto model)
        {
            var user =  new User(model.Id, model.Name, model.Email);
            user.Roles.AddRange(model.Roles.Select(r => new Role(string.Empty,r , new List<string>())));
            return user;
        }
    }
}
