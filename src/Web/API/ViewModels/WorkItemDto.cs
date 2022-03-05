using Domain.Entities;

namespace API.ViewModels
{
    public class WorkItemDto
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Iteration { get; set; } = string.Empty; 

        public string AssignedTo { get; set; } = string.Empty;

        public DateTime? StartDate { get; set; }

        public int Priority { get; set; }
    }
}
