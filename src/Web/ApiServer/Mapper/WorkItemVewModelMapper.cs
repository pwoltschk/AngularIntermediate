using ApiServer.ViewModels;
using Domain.Entities;

namespace ApiServer.Mapper;
public class WorkItemVewModelMapper : IMapper<WorkItemsViewModel, IEnumerable<WorkItem>>
{
    private readonly IMapper<WorkItemDto, WorkItem> _mapper;

    public WorkItemVewModelMapper(IMapper<WorkItemDto, WorkItem> mapper)
    {
        _mapper = mapper;
    }

    public WorkItemsViewModel Map(IEnumerable<WorkItem> model)
    {
        return new WorkItemsViewModel { WorkItems = model.Select(_mapper.Map).ToList() };
    }

    public IEnumerable<WorkItem> Map(WorkItemsViewModel model)
    {
        throw new NotImplementedException();
    }
}
