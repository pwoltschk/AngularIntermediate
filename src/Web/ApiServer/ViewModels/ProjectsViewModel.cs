namespace ApiServer.ViewModels;

public class ProjectsViewModel
{
    public string Organization { get; set; } = string.Empty;

    public List<ProjectDto> Projects { get; set; } = new();
}