using Application.Common.Services;

namespace Application.Users.Command;
public record DeleteUserCommand(string Id) : IRequest;

public class DeleteUserCommandHandler(IIdentityService identityService) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await identityService.DeleteUserAsync(request.Id);
    }
}
