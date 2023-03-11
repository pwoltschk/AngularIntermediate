namespace Application.Common.Services;
public class User
{
    public string Id { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public List<Role> Roles { get; private set; } = new();

    public User() { }
    public User(string id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
        Roles = new List<Role>();
    }
}


