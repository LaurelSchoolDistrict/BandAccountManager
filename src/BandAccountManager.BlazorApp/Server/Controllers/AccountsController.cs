using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using BandAccountManager.Core.Common;
using BandAccountManager.Core.Accounts;
using BandAccountManager.BlazorApp.Shared.Accounts;
using BandAccountManager.BlazorApp.Shared.Authorization;

namespace BandAccountManager.BlazorApp.Server.Controllers
{
    [Authorize]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;

        public AccountsController(IRepository<Account> accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [Authorize(Policy = Permissions.Account.Write)]
        [HttpPost]
        [Route("{accountId}/transactions")]
        public async Task<IActionResult> AddTransaction(string accountId, [FromBody]TransactionDto transactionDto)
        {
            var account = await _accountRepository.Get(accountId);

            if (account is null)
            {
                return NotFound();
            }

            account.Transactions.Add(_mapper.Map<Transaction>(transactionDto));

            await _accountRepository.Save(account);

            return Ok();
        }

        [Authorize(Policy = Permissions.Account.Write)]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody]AccountDto accountDto)
        {
            var existingAccount = await _accountRepository.Get(accountDto.Id);

            if (existingAccount is not null)
            {
                return Conflict();
            }

            await _accountRepository.Save(_mapper.Map<Account>(accountDto));

            return Ok();
        }

        [Authorize(Policy = Permissions.Account.Delete)]
        [HttpDelete]
        [Route("{accountId}")]
        public async Task Delete(string accountId)
        {
            await _accountRepository.Delete(accountId);
        }

        [Authorize(Policy = Permissions.Account.DeleteTransaction)]
        [HttpDelete]
        [Route("{accountId}/transactions/{transactionId}")]
        public async Task<IActionResult> DeleteTransaction(string accountId, string transactionId)
        {
            var account = await _accountRepository.Get(accountId);

            if (account is null)
            {
                return NotFound();
            }

            account.Transactions.RemoveAll(t => t.Id.Equals(transactionId));

            await _accountRepository.Save(account);

            return Ok();
        }

        [Authorize(Policy = Permissions.Account.Read)]
        [HttpGet]
        [Route("{accountId}")]
        public async Task<IActionResult> Get(string accountId)
        {
            var account = await _accountRepository.Get(accountId);

            if (account is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AccountDto>(account));
        }

        [HttpGet]
        [Route("_mine")]
        public async Task<IActionResult> GetMine()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var accounts = await _accountRepository.Query(a => a.Student.Id.Equals(""));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            if (accounts.Count == 0)
            {
                return NotFound();
            }

            else if (accounts.Count > 1)
            {
                return Conflict();
            }

            return Ok(_mapper.Map<AccountDto>(accounts.First()));
        }

        [Authorize(Policy = Permissions.Account.Read)]
        [HttpGet]
        [Route("{accountId}/transactions")]
        public async Task<IActionResult> GetTransactions(string accountId)
        {
            var account = await _accountRepository.Get(accountId);

            if (account is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TransactionDto[]>(account.Transactions.OrderByDescending(t => t.DateEffective)));
        }

        [Authorize(Policy = Permissions.Account.ReadAll)]
        [HttpGet]
        [Route("_search")]
        public async Task<AccountDto[]> Search(string query)
        {
            var results = await _accountRepository.Query(a =>
                a.Id.Contains(query, StringComparison.OrdinalIgnoreCase)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                || a.Student.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                || a.Student.EmailAddress.Contains(query, StringComparison.OrdinalIgnoreCase));

            return _mapper.Map<AccountDto[]>(results);
        }

        [Authorize(Policy = Permissions.Account.Write)]
        [HttpPut]
        [Route("{accountId}")]
        public async Task<IActionResult> Update([FromBody]AccountDto accountDto)
        {
            var account = await _accountRepository.Get(accountDto.Id);

            if (account is null)
            {
                return NotFound();
            }

            _mapper.Map(accountDto, account);

            await _accountRepository.Save(account);

            return Ok();
        }
    }
}
