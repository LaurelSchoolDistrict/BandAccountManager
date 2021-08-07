using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MudBlazor;
using MudBlazor.Services;

using BandAccountManager.BlazorApp.Client.Services;
using BandAccountManager.BlazorApp.Shared.Authorization;
using BandAccountManager.BlazorApp.Client.Authentication;

namespace BandAccountManager.BlazorApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Authentication", options.ProviderOptions);

                options.UserOptions.NameClaim = "name";
                options.UserOptions.RoleClaim = "https://my.spartan.band/role";
            }).AddAccountClaimsPrincipalFactory<CustomUserFactory>();

            builder.Services.AddAuthorizationCore(options =>
            {
                options.AddPolicy(Roles.Administrator, Roles.PolicyImplementations.AdministratorPolicy);
                options.AddPolicy(Roles.Teacher, Roles.PolicyImplementations.TeacherPolicy);
            });

            builder.Services.AddMudServices(options =>
            {
                var sc = options.SnackbarConfiguration;

                sc.ClearAfterNavigation = true;
                sc.HideTransitionDuration = (int)TimeSpan.FromMilliseconds(500).TotalMilliseconds;
                sc.MaxDisplayedSnackbars = 3;
                sc.PositionClass = Defaults.Classes.Position.BottomLeft;
                sc.PreventDuplicates = true;
                sc.ShowCloseIcon = true;
                sc.ShowTransitionDuration = (int)TimeSpan.FromMilliseconds(500).TotalMilliseconds;
                sc.SnackbarVariant = Variant.Filled;
                sc.VisibleStateDuration = (int)TimeSpan.FromSeconds(2).TotalMilliseconds;
            });

            builder.Services
                .AddHttpClient<AccountsApiClient>(hc => hc.BaseAddress = new(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            await builder.Build().RunAsync();
        }
    }
}
