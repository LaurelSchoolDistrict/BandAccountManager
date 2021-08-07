using System;
using System.Collections.Generic;
using System.Linq;

using BandAccountManager.Core.Common;

namespace BandAccountManager.Core.Accounts
{
    public class Account : BaseEntity, IAggregateRoot
    {
        public string StudentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public int StudentGraduationYear { get; set; }
        public List<string> ParentEmails { get; set; } = new();
        public List<Transaction> Transactions { get; set; } = new();

        public decimal Balance => (StartingBalance + Transactions
            .Where(t => t.DateEffective.UtcDateTime <= DateTimeOffset.UtcNow)
            .Sum(t => t.Amount));

        public decimal StartingBalance { get; set; }
        public DateTimeOffset? LastTransactionDate => Transactions.Any() ? Transactions.OrderByDescending(t => t.DateEffective).First().DateEffective : null;
        public decimal? LastTransactionAmount => Transactions.Any() ? Transactions.OrderByDescending(t => t.DateEffective).First().Amount : null;
    }
}
