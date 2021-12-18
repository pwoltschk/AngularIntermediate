using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public IEnumerable<WorkItem> WorkItems { get; set; } = Enumerable.Empty<WorkItem>();
    }
}
