﻿using Microsoft.AspNetCore.Components;
using UI.Components;

namespace UI.Pages;

public partial class WorkItems
{
    [CascadingParameter]
    public ProjectState State { get; set; } = null!;

    private WorkItemDto _newWorkItem = new();
    private WorkItemDto _editWorkItem = new();

    private WorkItemDialog _createWorkItemDialog = null!;
    private WorkItemDialog _editWorkItemDialog = null!;

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

    public void ShowEditWorkItemDialog(WorkItemDto item)
    {
        _editWorkItem = item;
        _editWorkItemDialog.Show();
    }

    public async Task AddWorkItem(WorkItemDto workItem)
    {
        var itemId = await State.WorkItemClient.PostWorkItemAsync(new CreateWorkItemRequest
        {
            ProjectId = State.SelectedList!.Id,
            Title = workItem.Title
        });
        workItem.Id = itemId;
        State.SelectedList!.WorkItems.Add(workItem);
    }

    public async Task UpdateWorkItem(WorkItemDto workItem)
    {
        await State.WorkItemClient.PutWorkItemAsync(workItem.Id, new UpdateWorkItemRequest
        {
            Id = workItem.Id,
            ProjectId = workItem.ProjectId,
            Title = workItem.Title,
            Description = workItem.Description,
            Iteration = workItem.Iteration,
            AssignedTo = workItem.AssignedTo,
            Priority = workItem.Priority,
            Stage = workItem.Stage,
        });
    }

    public async Task DeleteWorkItem(int id)
    {
        await State.WorkItemClient.DeleteWorkItemAsync(id);
        var workItem = State.SelectedList!.WorkItems.First(w => w.Id == id);
        State.SelectedList!.WorkItems.Remove(workItem);
    }

    private async Task UpdateWorkItemStage(WorkItemDto item, int stage)
    {
        item.Stage = stage;
        await UpdateWorkItem(item);
    }
}