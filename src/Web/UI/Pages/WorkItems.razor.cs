using Microsoft.AspNetCore.Components;

namespace UI.Pages
{
    public partial class WorkItems
    {
        [Inject]
        public IWorkItemClient WorkItemClient { get; set; } = null!;

        public WorkItemsViewModel? Model { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadWorkItems();
        }

        private async Task LoadWorkItems()
        {
            Model = await WorkItemClient.GetWorkItemsAsync();
        }

        private string GetStageName(int stage) => stage switch
        {
            0 => "Planned",
            1 => "In Progress",
            2 => "Completed",
            _ => "Unknown"
        };
    }
}
