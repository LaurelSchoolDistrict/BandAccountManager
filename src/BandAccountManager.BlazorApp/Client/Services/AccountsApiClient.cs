using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using MudBlazor;

using BandAccountManager.BlazorApp.Shared.Accounts;

namespace BandAccountManager.BlazorApp.Client.Services
{
    public class AccountsApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;

        public AccountsApiClient(HttpClient httpClient, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
        }

        public async ValueTask AddTransaction(string accountId, TransactionDto transactionDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/accounts/{accountId}/transactions", transactionDto);

            if (!response.IsSuccessStatusCode)
            {
                _snackbar.Add(response.ReasonPhrase, Severity.Error);

                return;
            }

            _snackbar.Add($"Transaction was successfully added to account {accountId}.", Severity.Success);
        }

        public async ValueTask Create(AccountDto accountDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/accounts", accountDto);

            if (!response.IsSuccessStatusCode)
            {
                _snackbar.Add(response.ReasonPhrase, Severity.Error);

                return;
            }

            _snackbar.Add($"Account {accountDto.Id} was successfully created.", Severity.Success);
        }

        public async ValueTask Delete(string accountId)
        {
            var response = await _httpClient.DeleteAsync($"/api/accounts/{accountId}");

            if (!response.IsSuccessStatusCode)
            {
                _snackbar.Add(response.ReasonPhrase, Severity.Error);

                return;
            }

            _snackbar.Add($"Account {accountId} was successfully deleted.", Severity.Success);
        }

        public async ValueTask DeleteTransaction(string accountId, string transactionId)
        {
            var response = await _httpClient.DeleteAsync($"/api/accounts/{accountId}/{transactionId}");

            if (!response.IsSuccessStatusCode)
            {
                _snackbar.Add(response.ReasonPhrase, Severity.Error);

                return;
            }

            _snackbar.Add($"Transaction was successfully deleted from account {accountId}.", Severity.Success);
        }

        public async Task<AccountDto?> Get(string accountId)
        {
            var response = await _httpClient.GetAsync($"/api/accounts/{accountId}");

            if (!response.IsSuccessStatusCode)
            {
                _snackbar.Add(response.ReasonPhrase, Severity.Error);

                return null;
            }

            return await response.Content.ReadFromJsonAsync<AccountDto>();
        }

        public async Task<IReadOnlyCollection<TransactionDto>> GetTransactions(string accountId)
        {
            var response = await _httpClient.GetAsync($"/api/accounts/{accountId}/transactions");

            if (!response.IsSuccessStatusCode)
            {
                _snackbar.Add(response.ReasonPhrase, Severity.Error);

                return Array.Empty<TransactionDto>();
            }

            return await response.Content.ReadFromJsonAsync<TransactionDto[]>() ?? Array.Empty<TransactionDto>();
        }

        public async Task<AccountDto[]> Search(string query)
        {
            var response = await _httpClient.GetAsync($"/api/accounts/_search?query={query}");

            if (!response.IsSuccessStatusCode)
            {
                _snackbar.Add(response.ReasonPhrase, Severity.Error);

                return Array.Empty<AccountDto>();
            }

            return await response.Content.ReadFromJsonAsync<AccountDto[]>() ?? Array.Empty<AccountDto>();
        }

        public async ValueTask Update(AccountDto accountDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/accounts/{accountDto.Id}", accountDto);

            if (!response.IsSuccessStatusCode)
            {
                _snackbar.Add(response.ReasonPhrase, Severity.Error);

                return;
            }

            _snackbar.Add($"Account {accountDto.Id} was successfully updated.", Severity.Success);
        }
    }
}
