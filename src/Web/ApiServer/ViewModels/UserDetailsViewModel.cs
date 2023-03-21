namespace ApiServer.ViewModels;
public class UserDetailsViewModel
{
    public List<RoleDto> Roles { get; set; } = new();
    public UserDto User { get; set; } = new UserDto();
}