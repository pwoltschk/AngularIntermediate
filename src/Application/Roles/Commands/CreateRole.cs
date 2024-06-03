using Application.Common.Services;

namespace Application.Roles.Commands;

public record CreateRoleCommand(Role Role) : IRequest;

public class CreateRoleCommandHandler(IIdentityService identityService) : IRequestHandler<CreateRoleCommand>
{
    public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        await identityService.CreateRoleAsync(request.Role);
    }
}