﻿namespace Shared.Identity;

public class Permission
{
    public const string ReadRoles = nameof(ReadRoles);
    public const string WriteRoles = nameof(WriteRoles);
    public const string WriteUsers = nameof(WriteUsers);
    public const string ReadUsers = nameof(ReadUsers);
    public const string ReadProjects = nameof(ReadProjects);
    public const string WriteProjects = nameof(WriteProjects);

    public static readonly IReadOnlyCollection<string> AllPermissions = new List<string>
        {
            ReadRoles,
            WriteRoles,
            WriteUsers,
            ReadUsers,
            ReadProjects,
            WriteProjects
        }.AsReadOnly();

}

