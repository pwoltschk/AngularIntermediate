using Application.Common.Services;

namespace Application.Users.Command;
public record UpdateUserCommand(User User) : IRequest;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IIdentityService _identityService;

    public UpdateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _identityService.UpdateUserAsync(request.User);
    }
}
