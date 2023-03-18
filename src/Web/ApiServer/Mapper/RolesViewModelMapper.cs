using ApiServer.ViewModels;
using Application.Common.Services;

namespace ApiServer.Mapper
{
    public class RolesViewModelMapper : IMapper<RolesViewModel, IEnumerable<Role>>
    {
        public RolesViewModel Map(IEnumerable<Role> model)
        {
            return new RolesViewModel
            {
                Roles = model.Select(x => new RoleDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Permissions = x.Permissions.ToList(),
                }).ToList(),
            };
        }
    }
}
