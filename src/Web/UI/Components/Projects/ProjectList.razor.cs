using Microsoft.AspNetCore.Components;

namespace UI.Components.Projects;
public partial class ProjectList
{
    [Parameter]
    public List<ProjectDto> Projects { get; set; } = new();

    [Parameter]
    public ProjectDto? SelectedProject { get; set; }

    [Parameter]
    public EventCallback<ProjectDto> OnSelectProject { get; set; }

    [Parameter]
    public EventCallback OnCreateProject { get; set; }

    [Parameter]
    public EventCallback<ProjectDto> OnEditProject { get; set; }

    [Parameter]
    public EventCallback<ProjectDto> OnDeleteProject { get; set; }

    private bool IsSelected(ProjectDto project)
    {
        return SelectedProject?.Id == project.Id;
    }
}
