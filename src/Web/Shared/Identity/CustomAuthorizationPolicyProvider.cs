using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Shared.Identity
{
    public class CustomAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly ConcurrentDictionary<string, AuthorizationPolicy> _policyCache = new ConcurrentDictionary<string, AuthorizationPolicy>();

        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options)
        {
        }

        public override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            return _policyCache.TryGetValue(policyName, out var policy) ? Task.FromResult(policy) : CreateAndCachePolicyAsync(policyName);
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