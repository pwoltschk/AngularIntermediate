namespace Domain.Entities;
public class Role
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public IList<string> Permissions { get; }

    public Role(string id, string name, IList<string> permissions)
    {
        Id = id;
        Name = name;
        Permissions = permissions;
    }
}
