using Blazored.LocalStorage;
using InventaryApp.Web.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InventaryApp.Web
{
    public class LocalAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storeService;

        public LocalAuthenticationStateProvider(ILocalStorageService storageService)
        {
            _storeService = storageService;
        }
        public  async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _storeService.ContainKeyAsync("User"))
            {
                var userInfo = await _storeService.GetItemAsync<LocalUserInfo>("User");

                var claims = new[]
                {
                    new Claim("Id",userInfo.Id),
                    new Claim("Email", userInfo.Email),
                    new Claim("FirstName", userInfo.FirstName),
                    new Claim("LastName", userInfo.LastName),
                    new Claim("Token", userInfo.Token)
                };

                var identity = new ClaimsIdentity(claims, "BearerToken");
                var user = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }

            return new AuthenticationState(new ClaimsPrincipal());
        }

        public async Task LogoutAsync()
        {
            await _storeService.RemoveItemAsync("User");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }
    }
}
