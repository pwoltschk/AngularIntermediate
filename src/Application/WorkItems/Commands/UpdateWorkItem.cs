using Application.WorkItems.Requests;
using Domain.Events;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Application.WorkItems.Commands;

public record UpdateWorkItemCommand(UpdateWorkItemRequest Item) : IRequest;

public class UpdateWorkItemCommandHandler : AsyncRequestHandler<UpdateWorkItemCommand>
{
    private readonly IRepository<WorkItem> _repository;

    public UpdateWorkItemCommandHandler(IRepository<WorkItem> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Item.Id, cancellationToken)
            ?? throw new Exception($"The request ID {request.Item.Id} was not found.");

        if (HasWorkItemBeenAssigned(entity, request))
        {
            entity.AddEvent(new WorkItemAssignedDomainEvent(entity.Id));
        }

        entity.ProjectId = request.Item.ProjectId;
        entity.Title = request.Item.Title;
        entity.AssignedTo = request.Item.AssignedTo;
        entity.Iteration = request.Item.Iteration;
        entity.Priority = Priority.FromLevel(request.Item.Priority);
        entity.Description = request.Item.Description;
        entity.StartDate = request.Item.StartDate;
        entity.Stage = Stage.FromId(request.Item.Stage);

        await _repository.UpdateAsync(entity, cancellationToken);
    }

    private static bool HasWorkItemBeenAssigned(WorkItem entity, UpdateWorkItemCommand request)
    {
        return entity.AssignedTo != request.Item.AssignedTo && !string.IsNullOrEmpty(request.Item.AssignedTo);
    }
}
