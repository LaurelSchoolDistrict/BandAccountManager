using System;

using BandAccountManager.Core.Common;

namespace BandAccountManager.Core.Accounts
{
    public class Transaction : IEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset DateEntered { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset DateEffective { get; set; } = DateTimeOffset.UtcNow;
        public decimal Amount { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
