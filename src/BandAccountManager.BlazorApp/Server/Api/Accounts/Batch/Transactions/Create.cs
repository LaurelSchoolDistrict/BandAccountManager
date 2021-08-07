using System;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using BandAccountManager.BlazorApp.Shared.Accounts;
using BandAccountManager.Core.Accounts;
using BandAccountManager.Core.Common;
using BandAccountManager.BlazorApp.Shared.Authorization;

namespace BandAccountManager.BlazorApp.Server.Api.Accounts.Batch.Transactions
{
    public class CreateRequest
    {
        [FromQuery(Name = "accountIds")]
        public string AccountIds { get; set; } = string.Empty;

        [FromQuery(Name = "distributionStrategy")]
        public TransactionDistributionStrategyDto DistributionStrategy { get; set; }

        [FromBody]
        public TransactionDto Transaction { get; set; } = new();
    }

    public class Create : BaseAsyncEndpoint
        .WithRequest<CreateRequest>
        .WithoutResponse
    {
        private readonly IRepository<Account> _accountRepository;

        public Create(IRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Authorize(nameof(Policies.Teacher))]
        [HttpPost("api/accounts/_batch/transactions")]
        public override async Task<ActionResult> HandleAsync(CreateRequest request, CancellationToken cancellationToken = default)
        {
            decimal amountPerAccount = request.DistributionStrategy switch
            {
                TransactionDistributionStrategyDto.DivideAcrossAccounts => request.Transaction.Amount / request.AccountIds.Length,
                TransactionDistributionStrategyDto.FixedAmountToAccounts => request.Transaction.Amount,
                _ => throw new Exception()
            };


            foreach (var accountId in request.AccountIds.Split(new[] { ',' }, StringSplitOptions.TrimEntries))
            {
                var account = await _accountRepository.Get(accountId);

                if (account is null)
                {
                    continue;
                }

                account.Transactions.Add(new()
                {
                    Amount = amountPerAccount,
                    DateEffective = request.Transaction.DateEffective,
                    Id = Guid.NewGuid().ToString(),
                    Note = request.Transaction.Note
                });

                await _accountRepository.Save(account);
            }

            return Ok();
        }
    }
}
