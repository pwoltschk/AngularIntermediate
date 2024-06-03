using Application.Common.Services;

namespace Application.Roles.Commands;
public record DeleteRoleCommand(string Id) : IRequest;

public class DeleteRoleCommandHandler(IIdentityService identityService) : IRequestHandler<DeleteRoleCommand>
{
    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        await identityService.DeleteRoleAsync(request.Id);
    }
}
