using Application.Common.Services;

namespace Application.Users.Command;
public record UpdateUserCommand(User User) : IRequest;

public class UpdateUserCommandHandler(IIdentityService identityService) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await identityService.UpdateUserAsync(request.User);
    }
}
