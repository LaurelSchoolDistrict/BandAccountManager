using System;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using BandAccountManager.BlazorApp.Shared.Authorization;

namespace BandAccountManager.BlazorApp.Server.Api.Accounts
{
    public class UpdateRequest
    {

    }

    public class Update : BaseAsyncEndpoint
        .WithRequest<UpdateRequest>
        .WithoutResponse
    {
        [Authorize(Roles.Teacher)]
        [HttpPut("api/accounts/{accountId}")]
        public override async Task<ActionResult> HandleAsync([FromRoute] UpdateRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
