using Application.Common.Services;

namespace Application.Users.Queries;

public record GetUsersQuery : IRequest<IEnumerable<User>>;

public class GetUsersQueryHandler(IIdentityService identityService) : IRequestHandler<GetUsersQuery, IEnumerable<User>>
{
    public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await identityService.GetUsersAsync(cancellationToken);
    }
}