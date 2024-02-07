using Microsoft.AspNetCore.Components;

namespace UI.Pages;

public partial class ProjectState
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    [Inject]
    public IProjectClient ProjectsClient { get; set; } = null!;


    public ProjectsViewModel? Model { get; set; }

    private ProjectDto? _selectedList;

    public ProjectDto? SelectedList
    {
        get { return _selectedList; }
        set
        {
            _selectedList = value;
            StateHasChanged();
        }
    }

    public bool Initialised { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await ProjectsClient.GetProjectsAsync();
        SelectedList = Model.Projects.First();
        Initialised = true;
    }
}