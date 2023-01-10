using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public class AuthorService : BaseHttpService, IAuthorService
    {
        private readonly IClient _client;

        public AuthorService(IClient client, ILocalStorageService localStorageService)
            : base(client, localStorageService)
        {
            _client = client;
        }

        public async Task<Response<int>> CreateAuthorAsync(AuthorCreateDto author)
        {
            try
            {
                await GetBearerTokenAsync();
                await _client.AuthorsPOSTAsync(author);
                return new Response<int>();
            }
            catch (ApiException apiException)
            {
                return ConvertAPIExceptions<int>(apiException);
            }
        }

        public async Task<Response<int>> DeleteAuthorAsync(int authorId)
        {
            try
            {
                await GetBearerTokenAsync();
                await _client.AuthorsDELETEAsync(authorId);
                return new Response<int>();
            }
            catch (ApiException apiException)
            {
                return ConvertAPIExceptions<int>(apiException);
            }
        }

        public async Task<Response<AuthorDetailsDto>> GetAuthorByIdAsync(int id)
        {
            try
            {
                await GetBearerTokenAsync();
                var author = await _client.AuthorsGETAsync(id);
                return new Response<AuthorDetailsDto>
                {
                    Data = author,
                    Success = true,
                };
            }
            catch (ApiException apiException)
            {
                return ConvertAPIExceptions<AuthorDetailsDto>(apiException);
            }
        }

        public async Task<Response<List<AuthorReadOnlyDto>>> GetAuthorsAsync()
        {
            Response<List<AuthorReadOnlyDto>> response;
            try
            {
                await GetBearerTokenAsync();
                var authors = await _client.AuthorsAllAsync();
                response = new Response<List<AuthorReadOnlyDto>>
                {
                    Data = authors.ToList(),
                    Success = true,
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertAPIExceptions<List<AuthorReadOnlyDto>>(apiException);
            }
            return response;
        }

        public async Task<Response<int>> UpdateAuthorAsync(int id, AuthorUpdateDto author)
        {
            try
            {
                await GetBearerTokenAsync();
                await _client.AuthorsPUTAsync(id, author);
                return new Response<int>();
            }
            catch (ApiException apiException)
            {
                return ConvertAPIExceptions<int>(apiException);
            }
        }
    }
}
