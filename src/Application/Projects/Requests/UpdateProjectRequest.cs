namespace Application.Projects.Requests;

public class UpdateProjectRequest
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
}
