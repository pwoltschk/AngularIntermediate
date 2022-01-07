using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkItems.Requests
{
    public class UpdateWorkItemRequest
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Iteration { get; set; } = string.Empty; //Todo implement iterations

        public string AssignedTo { get; set; } = string.Empty; //Todo implement assigning user

        public int Priority { get; set; }
    }
}
