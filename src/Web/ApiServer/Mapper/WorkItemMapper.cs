using ApiServer.ViewModels;
using Domain.Entities;

namespace ApiServer.Mapper;

public class WorkItemMapper : IMapper<WorkItemDto, WorkItem>
{
    public WorkItemDto Map(WorkItem model)
    {
        return new WorkItemDto
        {
            Id = model.Id,
            Title = model.Title,
            ProjectId = model.ProjectId,
            AssignedTo = model.AssignedTo,
            Description = model.Description,
            Iteration = model.Iteration,
            StartDate = model.StartDate,
            Priority = model.Priority.Level,
            Stage = model.Stage.Id,
        };
    }

    public WorkItem Map(WorkItemDto model)
    {
        throw new NotImplementedException();
    }
}