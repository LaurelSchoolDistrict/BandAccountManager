using System.Threading;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using BandAccountManager.BlazorApp.Shared.Accounts;
using BandAccountManager.BlazorApp.Shared.Authorization;
using BandAccountManager.BlazorApp.Shared.Authorization.Requirements;
using BandAccountManager.Core.Accounts;
using BandAccountManager.Core.Common;

namespace BandAccountManager.BlazorApp.Server.Api.Accounts
{
    public class GetRequest
    {
        [FromRoute(Name = "accountId")]
        public string AccountId { get; set; } = string.Empty;
    }

    public class Get : BaseAsyncEndpoint
        .WithRequest<GetRequest>
        .WithResponse<AccountDto>
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public Get(IRepository<Account> accountRepository, IAuthorizationService authorizationService, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("api/accounts/{accountId}")]
        public override async Task<ActionResult<AccountDto>> HandleAsync([FromRoute] GetRequest request, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.Get(request.AccountId);

            if (account is null)
            {
                return NotFound();
            }

            var teacherAuthResult = await _authorizationService.AuthorizeAsync(User, nameof(Policies.Teacher));

            if (!teacherAuthResult.Succeeded)
            {
                var sameStudentAuthResult = await _authorizationService.AuthorizeAsync(User, account, SameStudentRequirement.Instance);

                if (!sameStudentAuthResult.Succeeded)
                {
                    return Unauthorized();
                }
            }

            return _mapper.Map<AccountDto>(account);
        }
    }
}
