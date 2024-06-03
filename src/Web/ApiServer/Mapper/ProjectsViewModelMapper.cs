using ApiServer.ViewModels;
using Domain.Entities;

namespace ApiServer.Mapper;

public class ProjectsViewModelMapper(IMapper<WorkItemDto, WorkItem> mapper)
    : IMapper<ProjectsViewModel, IEnumerable<Project>>
{
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
            WorkItems = model.WorkItems.Select(mapper.Map).ToList()
        };
    }

    public IEnumerable<Project> Map(ProjectsViewModel model)
    {
        throw new NotImplementedException();
    }
}
