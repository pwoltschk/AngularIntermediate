using Application.Common.Services;

namespace Application.Users.Queries;
public record GetUserQuery(string Id) : IRequest<User>;

public class GetUserQueryHandler(IIdentityService identityService) : IRequestHandler<GetUserQuery, User>
{
    public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await identityService.GetUserAsync(request.Id);
    }
}

