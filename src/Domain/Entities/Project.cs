namespace Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public IEnumerable<WorkItem> WorkItems { get; set; } = Enumerable.Empty<WorkItem>();
    }
}
