using Microsoft.AspNetCore.Components;

namespace UI.Pages;

public partial class Project
{
    [CascadingParameter]
    public ProjectState State { get; set; } = null!;

    private bool IsSelected(ProjectDto list)
    {
        return State.SelectedList!.Id == list.Id;
    }

    private void SelectList(ProjectDto list)
    {
        if (IsSelected(list)) return;

        State.SelectedList = list;
    }
}