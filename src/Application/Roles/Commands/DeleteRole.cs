using Application.Common.Services;

namespace Application.Roles.Commands;
public record DeleteRoleCommand(string Id) : IRequest;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IIdentityService _identityService;

    public DeleteRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        await _identityService.DeleteRoleAsync(request.Id);
    }
}
