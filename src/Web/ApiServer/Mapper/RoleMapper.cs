using ApiServer.ViewModels;
using Domain.Entities;

namespace ApiServer.Mapper;

public class RoleMapper : IMapper<RoleDto, Role>
{
    public Role Map(RoleDto model)
    {
        return new Role(model.Id, model.Name, model.Permissions);
    }

    public RoleDto Map(Role model)
    {
        return new RoleDto
        {
            Id = model.Id,
            Name = model.Name,
            Permissions = [.. model.Permissions]
        };
    }
}