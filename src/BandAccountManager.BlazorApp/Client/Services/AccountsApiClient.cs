using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using MudBlazor;

using BandAccountManager.BlazorApp.Shared.Accounts;
using Microsoft.AspNetCore.Components.Forms;

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

        public async Task AddMultiAccountTransaction(IEnumerable<string> accountIds, TransactionDto transaction, TransactionDistributionStrategyDto distributionStrategy)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/accounts/_batch/transactions?distributionStrategy={distributionStrategy}&accountIds={string.Join(',', accountIds)}", transaction);

            await _snackbar.AddHttpResultMessage(response, "Multi-account transaction successfully added.");
        }

        public async ValueTask AddTransaction(string accountId, TransactionDto transaction)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/accounts/{accountId}/transactions", transaction);

            if (!response.IsSuccessStatusCode)
            {
                _snackbar.Add(response.ReasonPhrase, Severity.Error);

                return;
            }

            _snackbar.Add($"Transaction was successfully added to account {accountId}.", Severity.Success);
        }

        public async ValueTask Create(NewAccountDto newAccount)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/accounts", newAccount);

            if (!response.IsSuccessStatusCode)
            {
                _snackbar.Add(response.ReasonPhrase, Severity.Error);

                return;
            }

            _snackbar.Add($"Account {newAccount.Id} was successfully created.", Severity.Success);
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
            var response = await _httpClient.DeleteAsync($"/api/accounts/{accountId}/transactions/{transactionId}");

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

        public async Task<IReadOnlyCollection<AccountRefDto>> GetAll(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetAsync($"/api/accounts?pageNumber={pageNumber}&pageSize={pageSize}");

            if (!response.IsSuccessStatusCode)
            {
                _snackbar.Add(await response.Content.ReadAsStringAsync(), Severity.Error);

                return Array.Empty<AccountRefDto>();
            }

            return await response.Content.ReadFromJsonAsync<AccountRefDto[]>() ?? Array.Empty<AccountRefDto>();
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

        public async Task ImportCsv(Stream csvFile)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(csvFile), "file", "upload.csv");

            var response = await _httpClient.PostAsync("/api/accounts/_batch/$importCsv", content);

            await _snackbar.AddHttpResultMessage(response, "Bulk upload completed!");
        }

        public async ValueTask Update(AccountDto account)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/accounts/{account.Id}", account);

            if (!response.IsSuccessStatusCode)
            {
                _snackbar.Add(response.ReasonPhrase, Severity.Error);

                return;
            }

            _snackbar.Add($"Account {account.Id} was successfully updated.", Severity.Success);
        }
    }
}
