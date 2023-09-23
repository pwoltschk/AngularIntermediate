using Microsoft.AspNetCore.Components;

namespace UI.Components;

public partial class DeleteProjectDialog
{
    [Parameter]
    public bool Show { get; set; }

    [Parameter]
    public EventCallback<bool> ShowChanged { get; set; }

    [Parameter]
    public ProjectDto Project { get; set; } = new();

    [Parameter]
    public EventCallback<ProjectDto> OnDelete { get; set; }

    private Task OnClose() => ShowChanged.InvokeAsync(false);

    private async Task HandleDelete()
    {
        await OnDelete.InvokeAsync(Project);
        await ShowChanged.InvokeAsync(false);
    }
}