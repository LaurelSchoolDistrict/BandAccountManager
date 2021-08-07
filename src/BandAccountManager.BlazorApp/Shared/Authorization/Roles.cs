using Microsoft.AspNetCore.Authorization;

namespace BandAccountManager.BlazorApp.Shared.Authorization
{
    public static class Roles
    {
        /// <summary>
        /// Can do anything and everything in the app.
        /// </summary>
        public const string Administrator = nameof(Administrator);

        /// <summary>
        /// Can create accounts and add/remove transactions in accounts.
        /// </summary>
        public const string Teacher = nameof(Teacher);

        public static class PolicyImplementations
        {
            public static AuthorizationPolicy AdministratorPolicy => new AuthorizationPolicyBuilder()
                .RequireRole(Administrator)
                .Build();

            public static AuthorizationPolicy TeacherPolicy => new AuthorizationPolicyBuilder()
                .RequireRole(Administrator, Teacher)
                .Build();
        }
    }
}
