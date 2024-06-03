using Domain.Primitives;

namespace Application.Projects.Commands;

public record DeleteProjectCommand(int Id) : IRequest;

public class DeleteProjectCommandHandler(IRepository<Project> repository) : IRequestHandler<DeleteProjectCommand>
{
    public async Task Handle(DeleteProjectCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken) ?? throw new Exception($"The request ID {request.Id} was not found.");
        await repository.RemoveAsync(entity, cancellationToken);
    }
}