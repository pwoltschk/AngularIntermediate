using Domain.Primitives;

namespace Application.WorkItems.Commands;

public record DeleteWorkItemCommand(int Id) : IRequest;

public class DeleteWorkItemCommandHandler(IRepository<WorkItem> repository) : IRequestHandler<DeleteWorkItemCommand>
{
    public async Task Handle(DeleteWorkItemCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity != null)
        {
            await repository.RemoveAsync(entity, cancellationToken);
        }
    }
}