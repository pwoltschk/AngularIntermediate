using Application.Common.Services;

namespace Application.Users.Queries;

public record GetUsersQuery() : IRequest<IEnumerable<User>>;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
{
    private readonly IIdentityService _identityService;

    public GetUsersQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _identityService.GetUsersAsync(cancellationToken);
    }
}