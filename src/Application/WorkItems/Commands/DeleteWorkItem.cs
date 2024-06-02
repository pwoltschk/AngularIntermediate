using Domain.Primitives;

namespace Application.WorkItems.Commands;

public record DeleteWorkItemCommand(int Id) : IRequest;

public class DeleteWorkItemCommandHandler
    : IRequestHandler<DeleteWorkItemCommand>
{
    private readonly IRepository<WorkItem> _repository;

    public DeleteWorkItemCommandHandler(IRepository<WorkItem> repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteWorkItemCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity != null)
        {
            await _repository.RemoveAsync(entity, cancellationToken);
        }
    }
}