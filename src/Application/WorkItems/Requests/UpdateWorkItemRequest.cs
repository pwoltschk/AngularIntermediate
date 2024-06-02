namespace Application.WorkItems.Requests;

public class UpdateWorkItemRequest
{
    public int Id { get; init; }

    public int? ProjectId { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string Iteration { get; init; } = string.Empty;

    public string AssignedTo { get; init; } = string.Empty;

    public DateTime? StartDate { get; }

    public int Priority { get; init; }

    public int Stage { get; init; }
}