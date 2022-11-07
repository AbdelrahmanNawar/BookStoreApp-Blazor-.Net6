using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Constants;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookStoreApp.Blazor.Server.UI.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync() => await GetAuthenticationState();


        public async Task LoggedIn()
        {
            var authState = GetAuthenticationState();
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task LoggedOut()
        {
            await _localStorage.RemoveItemAsync(AuthenticationConstants.AccessToken);
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }

        private async Task<AuthenticationState> GetAuthenticationState()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            var savedToken = await _localStorage.GetItemAsync<string>(AuthenticationConstants.AccessToken);
            if (savedToken is null)
                return new AuthenticationState(user);

            var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            if (tokenContent.ValidTo < DateTime.Now)
                return new AuthenticationState(user);

            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            user = new ClaimsPrincipal(new ClaimsIdentity(claims, AuthenticationConstants.JWT));

            return new AuthenticationState(user);
        }
    }
}
