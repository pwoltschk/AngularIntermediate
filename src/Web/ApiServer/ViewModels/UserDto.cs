namespace ApiServer.ViewModels;
public class UserDto
{
    public string Id { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public IEnumerable<string> Roles { get; init; } = Enumerable.Empty<string>();

}
