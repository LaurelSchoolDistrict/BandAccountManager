using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace BandAccountManager.BlazorApp.Server.Authorization
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly IAuthorizationPolicyProvider _defaultProvider;

        public PermissionPolicyProvider(IOptions<AuthorizationOptions> authorizationOptions)
        {
            _defaultProvider = new DefaultAuthorizationPolicyProvider(authorizationOptions);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith("Permission.", StringComparison.OrdinalIgnoreCase))
            {
                var permissionName = policyName.Substring("Permission.".Length);

                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireClaim("permission", permissionName)
                    .Build();

                return Task.FromResult<AuthorizationPolicy?>(policy);
            }

            return _defaultProvider.GetPolicyAsync(policyName);
        }
    }
}
