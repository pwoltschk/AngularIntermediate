using Microsoft.AspNetCore.Components;
using UI.Components;

namespace UI.Pages
{
    public partial class WorkItems
    {
        [Inject]
        public IWorkItemClient WorkItemClient { get; set; } = null!;
        [Inject]
        public IUsersClient UsersClient { get; set; } = null!;
        [Inject]
        public IProjectClient ProjectsClient { get; set; } = null!;

        public WorkItemsViewModel? Model { get; set; }

        private List<UserDto> _users = new();
        private List<ProjectDto> _projects = new();
        private WorkItemDto _newWorkItem = new();
        private WorkItemDto _editWorkItem = new();

        private WorkItemDialog _createWorkItemDialog = null!;
        private WorkItemDialog _editWorkItemDialog = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadWorkItems();
            await LoadUsersAndProjects();
        }

        private async Task LoadWorkItems()
        {
            Model = await WorkItemClient.GetWorkItemsAsync();
        }

        private async Task LoadUsersAndProjects()
        {
            _users = (await UsersClient.GetUsersAsync()).Users.ToList();
            _projects = (await ProjectsClient.GetProjectsAsync()).Projects.ToList();
        }

        private string GetStageName(int stage) => stage switch
        {
            0 => "Planned",
            1 => "In Progress",
            2 => "Completed",
            _ => "Unknown"
        };

        public void ShowCreateWorkItemDialog()
        {
            _newWorkItem = new WorkItemDto();
            _createWorkItemDialog.Show();
        }

        public void ShowEditWorkItemDialog(WorkItemDto workItem)
        {
            _editWorkItem = workItem;
            _editWorkItemDialog.Show();
        }

        public async Task AddWorkItem(WorkItemDto workItem)
        {
            var itemId = await WorkItemClient.PostWorkItemAsync(new CreateWorkItemRequest
            {
                ProjectId = workItem.ProjectId,
                Title = workItem.Title,
            });
            workItem.Id = itemId;
            Model!.WorkItems.Add(workItem);
        }

        public async Task UpdateWorkItem(WorkItemDto workItem)
        {
            await WorkItemClient.PutWorkItemAsync(workItem.Id, new UpdateWorkItemRequest
            {
                Id = workItem.Id,
                ProjectId = workItem.ProjectId,
                Title = workItem.Title,
                Description = workItem.Description,
                Iteration = workItem.Iteration,
                AssignedTo = workItem.AssignedTo ?? string.Empty,
                Priority = workItem.Priority,
                Stage = workItem.Stage
            });

            await LoadWorkItems();
        }

        public async Task DeleteWorkItem(WorkItemDto workItem)
        {
            await WorkItemClient.DeleteWorkItemAsync(workItem.Id);
            Model!.WorkItems.Remove(workItem);
        }

        private string GetProjectName(int? projectId)
        {
            var project = _projects.FirstOrDefault(p => p.Id == projectId);
            return project != null ? project.Title : "UNTRACKED";
        }
    }
}