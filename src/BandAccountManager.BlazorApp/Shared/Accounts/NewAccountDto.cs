using System.Collections.Generic;

using BandAccountManager.Shared.Users;

namespace BandAccountManager.BlazorApp.Shared.Accounts
{
    public class NewAccountDto
    {
        public string Id => UserIdHelper.FromEmailAddress(StudentEmail);
        public string StudentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public decimal StartingBalance { get; set; }
    }
}
