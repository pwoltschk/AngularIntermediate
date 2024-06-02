using Application.Common.Services;

namespace Application.Roles.Commands;
public record UpdateRoleCommand(Role Role) : IRequest;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly IIdentityService _identityService;

    public UpdateRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        await _identityService.UpdateRoleAsync(request.Role);
    }
}

