using Blazored.LocalStorage;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services
{
    public class BookService : BaseHttpService, IBookService
    {
        private readonly IClient _client;

        public BookService(IClient client, ILocalStorageService localStorageService)
            : base(client, localStorageService)
        {
            _client = client;
        }

        public async Task<Response<int>> CreateBookAsync(BookCreateDto book)
        {
            try
            {
                await GetBearerTokenAsync();
                await _client.BooksPOSTAsync(book);
                return new Response<int>();
            }
            catch (ApiException apiException)
            {
                return ConvertAPIExceptions<int>(apiException);
            }
        }

        public async Task<Response<int>> DeleteBookAsync(int bookId)
        {
            try
            {
                await GetBearerTokenAsync();
                await _client.BooksDELETEAsync(bookId);
                return new Response<int>();
            }
            catch (ApiException apiException)
            {
                return ConvertAPIExceptions<int>(apiException);
            }
        }

        public async Task<Response<BookDetailsDto>> GetBookByIdAsync(int id)
        {
            try
            {
                await GetBearerTokenAsync();
                var book = await _client.BooksGETAsync(id);
                return new Response<BookDetailsDto>
                {
                    Data = book,
                    Success = true,
                };
            }
            catch (ApiException apiException)
            {
                return ConvertAPIExceptions<BookDetailsDto>(apiException);
            }
        }

        public async Task<Response<List<BookReadOnlyDto>>> GetBooksAsync()
        {
            Response<List<BookReadOnlyDto>> response;
            try
            {
                await GetBearerTokenAsync();
                var books = await _client.BooksAllAsync();
                response = new Response<List<BookReadOnlyDto>>
                {
                    Data = books.ToList(),
                    Success = true,
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertAPIExceptions<List<BookReadOnlyDto>>(apiException);
            }
            return response;
        }

        public async Task<Response<int>> UpdateBookAsync(int id, BookUpdateDto book)
        {
            try
            {
                await GetBearerTokenAsync();
                await _client.BooksPUTAsync(id, book);
                return new Response<int>();
            }
            catch (ApiException apiException)
            {
                return ConvertAPIExceptions<int>(apiException);
            }
        }
    }
}
