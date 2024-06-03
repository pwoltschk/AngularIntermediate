using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Shared.Identity
{
    public class CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        : DefaultAuthorizationPolicyProvider(options)
    {
        private readonly ConcurrentDictionary<string, AuthorizationPolicy> _policyCache = new();

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (_policyCache.TryGetValue(policyName, out AuthorizationPolicy? policy))
            {
                return policy;
            }

            policy = await CreateAndCachePolicyAsync(policyName);
            return policy;
        }

        private async Task<AuthorizationPolicy?> CreateAndCachePolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);

            if (policy != null || !Permission.AllPermissions.Contains(policyName))
            {
                return policy;
            }

            policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new CustomAuthorizationRequirement(policyName))
                .Build();

            _policyCache.TryAdd(policyName, policy);

            return policy;
        }
    }
}