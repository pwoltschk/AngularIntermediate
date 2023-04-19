using Application.Common.Services;

namespace Application.Users.Command;
public record UpdateUserCommand(User User) : IRequest;

public class UpdateUserCommandHandler : AsyncRequestHandler<UpdateUserCommand>
{
    private readonly IIdentityService _identityService;

    public UpdateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    protected override async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _identityService.UpdateUserAsync(request.User);
    }
}
