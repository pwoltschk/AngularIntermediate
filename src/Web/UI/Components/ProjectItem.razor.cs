using Microsoft.AspNetCore.Components;

namespace UI.Components;
public partial class ProjectItem
{
    [Parameter] 
    public ProjectDto Project { get; set; } = new();

    [Parameter] 
    public bool IsSelected { get; set; }

    [Parameter] 
    public EventCallback OnSelect { get; set; }

    [Parameter] 
    public EventCallback OnEdit { get; set; }

    [Parameter] 
    public EventCallback OnDelete { get; set; }
}
