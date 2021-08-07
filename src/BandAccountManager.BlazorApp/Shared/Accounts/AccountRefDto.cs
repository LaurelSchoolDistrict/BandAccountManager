using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BandAccountManager.BlazorApp.Shared.Accounts
{
    public class AccountRefDto
    {
        public string Id { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public decimal? LastTransactionAmount { get; set; }
        public DateTimeOffset? LastTransactionDate { get; set; }
    }
}
