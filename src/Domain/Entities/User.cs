using Domain.ValueObjects;

namespace Application.Common.Services;

public class User
{
    public string Id { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; } 
    public List<Role> Roles { get; private set; } = new();

    public User() { }

    public User(string id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = new Email(email); 
        Roles = new List<Role>();
    }

    public User(string id, string name, Email email) 
    {
        Id = id;
        Name = name;
        Email = email;
        Roles = new List<Role>();
    }
}
