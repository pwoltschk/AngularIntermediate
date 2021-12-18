using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WorkItem
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Iteration { get; set; } = string.Empty; //Todo implement iterations

        public string AssignedTo { get; set; } = string.Empty; //Todo implement assigning user

        public DateTime? StartDate { get; set; }

        public Priority Priority { get; set; }
    }
}
