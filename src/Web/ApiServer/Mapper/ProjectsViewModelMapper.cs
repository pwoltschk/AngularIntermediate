using ApiServer.ViewModels;
using Domain.Entities;

namespace ApiServer.Mapper;

public class ProjectsViewModelMapper : IMapper<ProjectsViewModel, IEnumerable<Project>>
{
    private readonly IMapper<WorkItemDto, WorkItem> _mapper;

    public ProjectsViewModelMapper(IMapper<WorkItemDto, WorkItem> mapper)
    {
        _mapper = mapper;
    }


    public ProjectsViewModel Map(IEnumerable<Project> model)
    {
        return new ProjectsViewModel
        {
            Projects = model.Select(Map).ToList()
        };
    }

    private ProjectDto Map(Project model)
    {
        return new ProjectDto
        {
            Id = model.Id,
            Title = model.Title,
            WorkItems = model.WorkItems.Select(_mapper.Map).ToList()
        };
    }

    public IEnumerable<Project> Map(ProjectsViewModel model)
    {
        throw new NotImplementedException();
    }
}
