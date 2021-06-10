using System;
using System.Collections.Generic;

namespace BandAccountManager.BlazorApp.Shared.Accounts
{
    public class AccountDto
    {
        public string Id { get; init; } = string.Empty;
        public DateTimeOffset DateCreated { get; set; }
        public AccountUserDto? Student { get; set; }
        public IReadOnlyList<AccountUserDto> Parents { get; set; } = Array.Empty<AccountUserDto>();
        public decimal Balance { get; set; }
        public decimal StartingBalance { get; set; }
    }
}
