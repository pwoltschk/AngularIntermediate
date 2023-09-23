using Application.WorkItems.Requests;
using Domain.Primitives;

namespace Application.WorkItems.Commands;
public record CreateWorkItemCommand(CreateWorkItemRequest Item) : IRequest<int>;

public class CreateWorkItemCommandHandler : IRequestHandler<CreateWorkItemCommand, int>
{
    private readonly IRepository<WorkItem> _repository;

    public CreateWorkItemCommandHandler(IRepository<WorkItem> repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new WorkItem
        {
            ProjectId = request.Item.ProjectId,
            Title = request.Item.Title
        };

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
