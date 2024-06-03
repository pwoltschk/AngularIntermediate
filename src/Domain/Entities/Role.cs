namespace Domain.Entities;
public class Role(string id, string name, IList<string> permissions)
{
    public string Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public IList<string> Permissions { get; } = permissions;
}
