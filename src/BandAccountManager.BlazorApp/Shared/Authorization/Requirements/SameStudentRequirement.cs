using System;

using Microsoft.AspNetCore.Authorization;

namespace BandAccountManager.BlazorApp.Shared.Authorization.Requirements
{
    public class SameStudentRequirement : IAuthorizationRequirement
    {
        private static Lazy<SameStudentRequirement> _instance = new(() => new());

        public static SameStudentRequirement Instance => _instance.Value;
    }
}
