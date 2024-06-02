namespace Application.WorkItems.Requests;

public class CreateWorkItemRequest
{
    public int? ProjectId { get; init; }

    public string Title { get; init; } = string.Empty;
}