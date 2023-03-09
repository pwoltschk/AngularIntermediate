using Application.Common.Services;

namespace Application.Roles.Queries;
public record GetRolesQuery : IRequest<IEnumerable<Role>>;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<Role>>
{
    private readonly IIdentityService _identityService;

    public GetRolesQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<IEnumerable<Role>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await _identityService.GetRolesAsync(cancellationToken);
    }
}
