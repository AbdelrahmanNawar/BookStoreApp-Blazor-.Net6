using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Constants;
using System.Net.Http.Headers;

namespace BookStoreApp.Blazor.Server.UI.Services.Base
{
    public class BaseHttpService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;

        public BaseHttpService(IClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        protected Response<Guid> ConvertAPIExceptions<Guid>(ApiException apiException)
        {
            if (apiException.StatusCode == 400)
                return new Response<Guid> { Message = "Validation errors have occured.", ValidationErrors = apiException.Response, Success = false };

            if (apiException.StatusCode == 404)
                return new Response<Guid> { Message = "The required item could not be found.", Success = false };

            if (apiException.StatusCode >= 200 && apiException.StatusCode <= 299)
                return new Response<Guid>();

            return new Response<Guid> { Message = "Something went wrong, please try again.", Success = false };
        }

        protected async Task GetBearerTokenAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(AuthenticationConstants.AccessToken);
            if (token is not null)
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationConstants.Bearer, token);
            }
        }
    }
}
