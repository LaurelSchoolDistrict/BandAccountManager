using System;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using BandAccountManager.BlazorApp.Shared.Accounts;
using BandAccountManager.BlazorApp.Shared.Authorization;
using BandAccountManager.Core.Accounts;
using BandAccountManager.Core.Common;

namespace BandAccountManager.BlazorApp.Server.Api.Accounts.Transactions
{
    public class CreateRequest
    {
        [FromRoute(Name = "accountId")]
        public string AccountId { get; set; } = string.Empty;

        [FromBody]
        public TransactionDto? Transaction { get; set; }
    }

    public class Create : BaseAsyncEndpoint
        .WithRequest<CreateRequest>
        .WithoutResponse
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;

        public Create(IRepository<Account> accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [Authorize(nameof(Policies.Teacher))]
        [HttpPost("api/accounts/{accountId}/transactions")]
        public override async Task<ActionResult> HandleAsync([FromRoute] CreateRequest request, CancellationToken cancellationToken = default)
        {
            if (request.Transaction is null)
            {
                return BadRequest();
            }

            var account = await _accountRepository.Get(request.AccountId);

            if (account is null)
            {
                return NotFound();
            }

            var transaction = _mapper.Map<Transaction>(request.Transaction);

            transaction.Id = Guid.NewGuid().ToString();

            account.Transactions.Add(transaction);

            await _accountRepository.Save(account);

            return Ok();
        }
    }
}
