using ApiServer.ViewModels;
using Domain.Entities;

namespace ApiServer.Mapper;
public class WorkItemViewModelMapper(IMapper<WorkItemDto, WorkItem> mapper)
    : IMapper<WorkItemsViewModel, IEnumerable<WorkItem>>
{
    public WorkItemsViewModel Map(IEnumerable<WorkItem> model)
    {
        return new WorkItemsViewModel { WorkItems = model.Select(mapper.Map).ToList() };
    }

    public IEnumerable<WorkItem> Map(WorkItemsViewModel model)
    {
        throw new NotImplementedException();
    }
}
