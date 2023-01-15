using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public interface IBookService
    {
        Task<Response<List<BookReadOnlyDto>>> GetBooksAsync();
        Task<Response<BookDetailsDto>> GetBookByIdAsync(int id);
        Task<Response<int>> CreateBookAsync(BookCreateDto book);
        Task<Response<int>> UpdateBookAsync(int id, BookUpdateDto book);
        Task<Response<int>> DeleteBookAsync(int bookId);
    }
}
