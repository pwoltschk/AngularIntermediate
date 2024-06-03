using ApiServer.ViewModels;
using Domain.Entities;

namespace ApiServer.Mapper;

public class RolesViewModelMapper(IMapper<RoleDto, Role> mapper) : IMapper<RolesViewModel, IEnumerable<Role>>
{
    public RolesViewModel Map(IEnumerable<Role> model)
    {
        return new RolesViewModel
        {
            Roles = model.Select(x => mapper.Map(x)).ToList()
        };
    }

    public IEnumerable<Role> Map(RolesViewModel model)
    {
        throw new NotImplementedException();
    }
}