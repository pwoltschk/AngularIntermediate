using Application.Common.Services;

namespace Application.Roles.Queries;
public record GetRolesQuery : IRequest<IEnumerable<Role>>;

public class GetRolesQueryHandler(IIdentityService identityService) : IRequestHandler<GetRolesQuery, IEnumerable<Role>>
{
    public async Task<IEnumerable<Role>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await identityService.GetRolesAsync(cancellationToken);
    }
}
