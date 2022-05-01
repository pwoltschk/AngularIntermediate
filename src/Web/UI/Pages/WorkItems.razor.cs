using Microsoft.AspNetCore.Components;

namespace UI.Pages;

public partial class WorkItems
{
    [CascadingParameter]
    public ProjectState State { get; set; } = null!;

    private WorkItemDto _newWorkItem = new();

    private bool _showCreateWorkItemDialog = false;

    public void ShowCreateWorkItemDialog()
    {
        _showCreateWorkItemDialog = true;
    }

    public void CloseCreateWorkItemDialog()
    {
        _showCreateWorkItemDialog = false;
    }

    public async Task AddWorkItem()
    {
        var itemId = await State.WorkItemClient.PostWorkItemAsync(new CreateWorkItemRequest()
        {
            ProjectId = State.SelectedList!.Id,
            Title = _newWorkItem.Title
        });
        var workItem = new WorkItemDto { Id = itemId, Title = _newWorkItem.Title };
        State.SelectedList!.WorkItems.Add(workItem);
        _newWorkItem = new WorkItemDto();
        _showCreateWorkItemDialog = false;
    }
}