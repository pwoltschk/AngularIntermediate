namespace ApiServer.ViewModels;

public class ProjectDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public List<WorkItemDto> WorkItems { get; set; } = new();
}