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
    }
}
