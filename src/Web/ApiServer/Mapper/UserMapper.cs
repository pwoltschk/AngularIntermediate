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
                Name = model.Name
            };
        }

        public User Map(UserDto model)
        {
            return new User(model.Email, model.Id, model.Name);
        }
    }
}
