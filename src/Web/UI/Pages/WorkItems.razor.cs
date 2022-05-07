using Microsoft.AspNetCore.Components;

namespace UI.Pages;

public partial class WorkItems
{
    [CascadingParameter]
    public ProjectState State { get; set; } = null!;

    private WorkItemDto _newWorkItem = new();
    private WorkItemDto _editWorkItem = new();

    private bool _showCreateWorkItemDialog = false;
    private bool _showEditWorkItemDialog = false;

    public void ShowCreateWorkItemDialog()
    {
        _showCreateWorkItemDialog = true;
    }

    public void CloseCreateWorkItemDialog()
    {
        _showCreateWorkItemDialog = false;
    }

    public void ShowEditWorkItemDialog(WorkItemDto item)
    {
        _editWorkItem = item;
        _showEditWorkItemDialog = true;
    }

    public void CloseEditWorkItemDialog()
    {
        _showEditWorkItemDialog = false;
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

    public async Task UpdateWorkItem()
    {
        await State.WorkItemClient.PutWorkItemAsync(_editWorkItem.Id, new UpdateWorkItemRequest()
        {
            Id = _editWorkItem.Id,
            ProjectId = _editWorkItem.ProjectId,
            Title = _editWorkItem.Title,
            Description = _editWorkItem.Description,
            Iteration = _editWorkItem.Iteration,
            AssignedTo = _editWorkItem.AssignedTo,
            Priority = _editWorkItem.Priority
        });
        _showEditWorkItemDialog = false;
    }

    public async Task DeleteWorkItem(int id)
    {
        await State.WorkItemClient.DeleteWorkItemAsync(id);
        var workItem = State.SelectedList!.WorkItems.First(w => w.Id == id);
        State.SelectedList!.WorkItems.Remove(workItem);
    }
}
