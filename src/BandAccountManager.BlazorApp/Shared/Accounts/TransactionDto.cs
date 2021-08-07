using System;

namespace BandAccountManager.BlazorApp.Shared.Accounts
{
    public class TransactionDto
    {
        public string Id { get; set; } = string.Empty;
        public DateTimeOffset DateEntered { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset DateEffective { get; set; } = DateTimeOffset.UtcNow;
        public decimal Amount { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
