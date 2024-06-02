namespace ApiServer.ViewModels;
public class UserDetailsViewModel
{
    public List<string> AllRoles { get; set; } = new();
    public List<RoleDto> Roles { get; init; } = new();
    public UserDto User { get; init; } = new();
}