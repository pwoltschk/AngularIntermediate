﻿using API.ViewModels;
using Domain.Entities;

namespace API.Mapper
{
    public class ProjectsViewModelMapper : IMapper<ProjectsViewModel, IEnumerable<Project>>
    {
        public ProjectsViewModel Map(IEnumerable<Project> model)
        {
            return new ProjectsViewModel()            {
                Projects = model.Select(Map).ToList()
            };
        }

        private ProjectDto Map(Project model)
        {
            return new ProjectDto
            {
                Id = model.Id, Title = model.Title, WorkItems = model.WorkItems.Select(Map).ToList()
            };
        }

        private WorkItemDto Map(WorkItem model)
        {
            return new WorkItemDto
            {
                Id = model.Id,
                Title = model.Title,
                ProjectId = model.ProjectId,
                AssignedTo = model.AssignedTo,
                Description = model.Description,
                Iteration = model.Iteration,
                StartDate = model.StartDate,
                Priority = (int)model.Priority,
            };
        }
    }
}
