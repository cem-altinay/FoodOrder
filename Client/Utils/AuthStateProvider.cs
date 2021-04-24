using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
namespace FoodOrder.Client.Utils
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationState anonymous;

        private readonly HttpClient client;
        public AuthStateProvider(ILocalStorageService localStorageService, HttpClient client)
        {
            this.localStorageService = localStorageService;
            anonymous = new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new ClaimsIdentity()));
            this.client = client;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await localStorageService.GetItemAsStringAsync("token");

            if (string.IsNullOrEmpty(token))
                return anonymous;

            string email = await localStorageService.GetItemAsStringAsync("email");
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }, "jwtAuthType"));

            client.DefaultRequestHeaders.Authorization= new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",token);
            return new AuthenticationState(claimsPrincipal);
        }

        public void NotifyUserLogin(string email)
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }, "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(claimsPrincipal));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}