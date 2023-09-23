using ApiServer.ViewModels;
using Domain.Entities;

namespace ApiServer.Mapper;

public class RolesViewModelMapper : IMapper<RolesViewModel, IEnumerable<Role>>
{
    private readonly IMapper<RoleDto, Role> _mapper;

    public RolesViewModelMapper(IMapper<RoleDto, Role> mapper)
    {
        _mapper = mapper;
    }

    public RolesViewModel Map(IEnumerable<Role> model)
    {
        return new RolesViewModel
        {
            Roles = model.Select(x => _mapper.Map(x)).ToList()
        };
    }

    public IEnumerable<Role> Map(RolesViewModel model)
    {
        throw new NotImplementedException();
    }
}