using Microsoft.AspNetCore.Components;

namespace UI.Pages;

public partial class Project
{
    [CascadingParameter]
    public ProjectState State { get; set; } = null!;

    [Inject]
    public IProjectClient ProjectsClient { get; set; } = null!;

    private bool _showProjectDialog = false;
    private bool _showDeleteProjectDialog = false;
    private ProjectDto _editProject = new();
    private ProjectDto _deleteProject = new();
    private bool _isEditMode = false;

    private void SelectList(ProjectDto list)
    {
        if (State.SelectedList?.Id == list.Id) return;
        State.SelectedList = list;
    }

    private void ShowCreateProjectDialog()
    {
        _isEditMode = false;
        _editProject = new ProjectDto();
        _showProjectDialog = true;
    }

    private void ShowEditProjectDialog(ProjectDto project)
    {
        _isEditMode = true;
        _editProject = project;
        _showProjectDialog = true;
    }

    private void ShowDeleteProjectDialog(ProjectDto project)
    {
        _deleteProject = project;
        _showDeleteProjectDialog = true;
    }

    private async Task HandleProjectSubmit(ProjectDto project)
    {
        if (_isEditMode)
        {
            await UpdateProject(project);
        }
        else
        {
            await CreateProject(project);
        }
    }

    private async Task CreateProject(ProjectDto project)
    {
        var projectId = await ProjectsClient.PostProjectAsync(new CreateProjectRequest
        {
            Title = project.Title
        });
        var newProject = new ProjectDto { Id = projectId, Title = project.Title, WorkItems = [] };
        State.Model!.Projects.Add(newProject);
    }

    private async Task UpdateProject(ProjectDto project)
    {
        await ProjectsClient.PutProjectAsync(project.Id, new UpdateProjectRequest
        {
            Title = project.Title,
            Id = project.Id
        });
        var existingProject = State.Model!.Projects.FirstOrDefault(p => p.Id == project.Id);
        if (existingProject != null)
        {
            existingProject.Title = project.Title;
        }
    }

    public async Task DeleteProject(ProjectDto project)
    {
        await ProjectsClient.DeleteProjectAsync(project.Id);
        State.Model!.Projects.Remove(project);
    }
}