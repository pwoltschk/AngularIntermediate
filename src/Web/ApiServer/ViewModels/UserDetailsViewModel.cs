namespace ApiServer.ViewModels;
public class UserDetailsViewModel
{
    public List<string> AllRoles { get; set; } = [];
    public List<RoleDto> Roles { get; init; } = [];
    public UserDto User { get; init; } = new();
}