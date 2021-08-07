using System;

using Microsoft.AspNetCore.Authorization;

namespace BandAccountManager.BlazorApp.Shared.Authorization
{
    public static class Policies
    {
        private static Lazy<AuthorizationPolicy> _administrator = new(() => Builders.Administrator.Build());
        private static Lazy<AuthorizationPolicy> _teacher = new(() => Builders.Teacher.Build());

        public static AuthorizationPolicy Administrator => _administrator.Value;
        public static AuthorizationPolicy Teacher => _teacher.Value;

        public static class Builders
        {
            public static AuthorizationPolicyBuilder Administrator => new AuthorizationPolicyBuilder()
                .RequireRole(Roles.Administrator);

            public static AuthorizationPolicyBuilder Teacher => new AuthorizationPolicyBuilder()
                .RequireRole(Roles.Administrator, Roles.Teacher);
        }
    }
}
