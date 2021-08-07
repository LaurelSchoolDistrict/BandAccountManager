using System.Threading;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using AutoMapper;

using BandAccountManager.BlazorApp.Shared.Accounts;
using BandAccountManager.BlazorApp.Shared.Authorization;
using BandAccountManager.Core.Accounts;
using BandAccountManager.Core.Common;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BandAccountManager.BlazorApp.Server.Api.Accounts
{
    public class CreateRequest
    {
        [FromBody]
        public NewAccountDto? NewAccount { get; set; }
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

        [Authorize(Roles.Teacher)]
        [HttpPost("api/accounts")]
        public override async Task<ActionResult> HandleAsync([FromRoute] CreateRequest request, CancellationToken cancellationToken = default)
        {
            if (request.NewAccount is null)
            {
                return BadRequest();
            }

            var account = await _accountRepository.Get(request.NewAccount.Id);

            if (account is not null)
            {
                return Conflict();
            }

            account = _mapper.Map<Account>(request.NewAccount);

            await _accountRepository.Save(account);

            return Created($"/api/accounts/{account.Id}", account);
        }
    }
}
