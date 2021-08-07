using System.Net.Http;
using System.Threading.Tasks;

namespace MudBlazor
{
    public static class ISnackbarExtensions
    {
        public static async Task AddHttpResultMessage(this ISnackbar snackbar, HttpResponseMessage response, string? successMessage = null)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                snackbar.Add(errorMessage, Severity.Error);
            }

            else if (!string.IsNullOrWhiteSpace(successMessage))
            {
                snackbar.Add(successMessage, Severity.Success);
            }
        }
    }
}
