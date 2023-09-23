using Domain.Primitives;

namespace Domain.Entities;

public class Project : AggregateRoot
{
    public string Title { get; set; } = string.Empty;

    public ICollection<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
}