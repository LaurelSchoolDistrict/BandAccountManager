using System;
using System.Collections.Generic;
using System.Linq;

using BandAccountManager.Core.Common;

namespace BandAccountManager.Core.Accounts
{
    public class Account : AggregateRootBase
    {
        public AccountUser? Student { get; set; }
        public List<AccountUser> Parents { get; set; } = new List<AccountUser>();
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public decimal Balance => (StartingBalance + Transactions.Sum(t => t.Amount));
        public decimal StartingBalance { get; set; }
    }
}
