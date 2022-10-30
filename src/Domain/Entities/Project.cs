using System.Collections.ObjectModel;

namespace Domain.Entities
{
    public class Project : AuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public ICollection<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
    }
}
