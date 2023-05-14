namespace ApiServer.ViewModels;
public class UserDto
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();

}
