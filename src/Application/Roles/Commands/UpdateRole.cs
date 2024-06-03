using Application.Common.Services;

namespace Application.Roles.Commands;
public record UpdateRoleCommand(Role Role) : IRequest;

public class UpdateRoleCommandHandler(IIdentityService identityService) : IRequestHandler<UpdateRoleCommand>
{
    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        await identityService.UpdateRoleAsync(request.Role);
    }
}

