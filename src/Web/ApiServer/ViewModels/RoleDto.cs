namespace ApiServer.ViewModels;
public class RoleDto
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public List<string> Permissions { get; init; } = new();
}
