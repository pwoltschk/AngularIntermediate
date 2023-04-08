namespace Application.Common.Services;
public class Role
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public IList<string> Permissions { get; private set; }

    public Role(string id, string name, IList<string> permissions)
    {
        Id = id;
        Name = name;
        Permissions = permissions;
    }

    public bool Has(string permission)
    {
        return Permissions.Contains(permission);
    }

    public void Update(string permission, bool grant)
    {
        if (grant && !Permissions.Contains(permission))
        {
            Permissions.Add(permission);
        }
        else
        {
            Permissions.Remove(permission);
        }
    }
}
