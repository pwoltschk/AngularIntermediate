using Microsoft.AspNetCore.Components;

namespace UI.Components;
public partial class ProjectDialog
{

    [Parameter]
    public bool Show { get; set; }

    [Parameter]
    public EventCallback<bool> ShowChanged { get; set; }

    [Parameter]
    public ProjectDto Project { get; set; } = new();

    [Parameter]
    public EventCallback<ProjectDto> OnSubmit { get; set; }

    [Parameter]
    public string Title { get; set; } = "Project";

    [Parameter]
    public string SubmitButtonText { get; set; } = "Submit";

    private Task OnClose() => ShowChanged.InvokeAsync(false);

    private async Task HandleSubmit()
    {
        await OnSubmit.InvokeAsync(Project);
        await ShowChanged.InvokeAsync(false);
    }
}
