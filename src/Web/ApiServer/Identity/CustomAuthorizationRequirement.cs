using Microsoft.AspNetCore.Authorization;

namespace ApiServer.Identity
{
    public class CustomAuthorizationRequirement : IAuthorizationRequirement
    {
        public CustomAuthorizationRequirement(string permission)
        {
            Permission = permission;
        }

        public string Permission { get; }
    }
}
