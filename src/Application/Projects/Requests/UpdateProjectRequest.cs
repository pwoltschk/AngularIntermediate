namespace Application.Projects.Requests;

public class UpdateProjectRequest
{
    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;
}
