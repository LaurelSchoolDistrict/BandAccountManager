using System;
using System.Collections.Generic;

using BandAccountManager.Shared.Users;

namespace BandAccountManager.BlazorApp.Shared.Accounts
{
    public class AccountDto
    {
        public string Id => UserIdHelper.FromEmailAddress(StudentEmail);
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
        public string StudentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public IReadOnlyList<string> ParentEmails { get; set; } = Array.Empty<string>();
        public decimal Balance { get; set; }
        public decimal StartingBalance { get; set; }
        public IReadOnlyCollection<TransactionDto> Transactions { get; set; } = Array.Empty<TransactionDto>();
    }
}
