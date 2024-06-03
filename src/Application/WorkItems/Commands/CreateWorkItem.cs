using Application.WorkItems.Requests;
using Domain.Primitives;

namespace Application.WorkItems.Commands;
public record CreateWorkItemCommand(CreateWorkItemRequest Item) : IRequest<int>;

public class CreateWorkItemCommandHandler(IRepository<WorkItem> repository)
    : IRequestHandler<CreateWorkItemCommand, int>
{
    public async Task<int> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new WorkItem
        {
            ProjectId = request.Item.ProjectId,
            Title = request.Item.Title
        };

        await repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
