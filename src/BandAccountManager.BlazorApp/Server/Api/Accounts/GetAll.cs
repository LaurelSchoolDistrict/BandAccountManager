using System;
using System.Collections.Generic;
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

namespace BandAccountManager.BlazorApp.Server.Api.Accounts
{
    public class GetAllRequest
    {
        [FromQuery(Name = "pageNumber")]
        public int? PageNumber { get; set; }

        [FromQuery(Name = "pageSize")]
        public int? PageSize { get; set; }
    }

    public class GetAll : BaseAsyncEndpoint
        .WithRequest<GetAllRequest>
        .WithResponse<AccountRefDto[]>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;

        public GetAll(
            IAuthorizationService authorizationService,
            IRepository<Account> accountRepository,
            IMapper mapper)
        {
            _authorizationService = authorizationService;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("api/accounts")]
        public override async Task<ActionResult<AccountRefDto[]>> HandleAsync([FromRoute] GetAllRequest request, CancellationToken cancellationToken = default)
        {
            int pageNumber = request.PageNumber ?? 1;
            int pageSize = request.PageSize ?? int.MaxValue;
            int skip = (pageSize * (pageNumber - 1));
            int take = pageSize;

            var userEmail = User.FindFirst("https://my.spartan.band/email")?.Value.ToLower() ?? string.Empty;

            // If the user is an admin or teacher, they can view all accounts and not just accounts they are assigned to.
            var authResult = await _authorizationService.AuthorizeAsync(User, Roles.Teacher);

            IReadOnlyCollection<Account> accounts = Array.Empty<Account>();

            if (authResult.Succeeded)
            {
                accounts = await _accountRepository.Query(new RepositoryQuery<Account>
                {
                    FilterExpression = a => true,
                    SortDirection = QuerySortDirection.Ascending,
                    SortExpression = a => a.StudentName
                }, skip, take);
            }

            else
            {
                accounts = await _accountRepository.Query(new RepositoryQuery<Account>
                {
                    FilterExpression = a => a.StudentEmail.ToLower() == userEmail || a.ParentEmails.Contains(userEmail),
                    SortDirection = QuerySortDirection.Ascending,
                    SortExpression = a => a.StudentName
                }, skip, take);
            }

            return _mapper.Map<AccountRefDto[]>(accounts);
        }
    }
}
