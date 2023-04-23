using Application.Common.Services;

namespace Application.Users.Command;
public record DeleteUserCommand(string Id) : IRequest;

public class DeleteUserCommandHandler : AsyncRequestHandler<DeleteUserCommand>
{
    private readonly IIdentityService _identityService;

    public DeleteUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    protected override async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _identityService.DeleteUserAsync(request.Id);
    }
}
