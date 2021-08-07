using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;

using BandAccountManager.BlazorApp.Shared.Authorization.Requirements;
using BandAccountManager.Core.Accounts;

namespace BandAccountManager.BlazorApp.Server.Authorization.Handlers
{
    public class SameStudentAccountAuthorizationHandler : AuthorizationHandler<SameStudentRequirement, Account>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameStudentRequirement requirement, Account resource)
        {
            var userEmail = context.User.FindFirst("email")?.Value ?? string.Empty;

            if (resource.StudentEmail.Equals(userEmail, StringComparison.OrdinalIgnoreCase) || resource.ParentEmails.Contains(userEmail, StringComparer.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
            }

            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
