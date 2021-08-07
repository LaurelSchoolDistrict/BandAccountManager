using System.Threading;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using BandAccountManager.BlazorApp.Shared.Authorization;
using BandAccountManager.Core.Accounts;
using BandAccountManager.Core.Common;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BandAccountManager.BlazorApp.Server.Api.Accounts.Transactions
{
    public class DeleteRequest
    {
        [FromRoute(Name = "accountId")]
        public string AccountId { get; set; } = string.Empty;

        [FromRoute(Name = "transactionId")]
        public string TransactionId { get; set; } = string.Empty;
    }

    public class Delete : BaseAsyncEndpoint
        .WithRequest<DeleteRequest>
        .WithoutResponse
    {
        private readonly IRepository<Account> _accountRepository;

        public Delete(IRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Authorize(nameof(Policies.Teacher))]
        [HttpDelete("api/accounts/{accountId}/transactions/{transactionId}")]
        public override async Task<ActionResult> HandleAsync([FromRoute] DeleteRequest request, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.Get(request.AccountId);

            if (account is null)
            {
                return NotFound();
            }

            account.Transactions.RemoveAll(t => t.Id.Equals(request.TransactionId));

            await _accountRepository.Save(account);

            return Ok();
        }
    }
}
