using Application.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ApiServer.Identity
{

    public class CustomAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;
        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options)
        {
            _options = options.Value;
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);

            if (policy == null && Permission.AllPermissions.Contains(policyName))
            {
                policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new CustomAuthorizationRequirement(policyName))
                .Build();

                _options.AddPolicy(policyName, policy);
            }

            return policy;
        }
    }


}
