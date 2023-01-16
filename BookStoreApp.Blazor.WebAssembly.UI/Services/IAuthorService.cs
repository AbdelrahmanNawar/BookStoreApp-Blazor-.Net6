using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadOnlyDto>>> GetAuthorsAsync();
        Task<Response<AuthorDetailsDto>> GetAuthorByIdAsync(int id);
        Task<Response<int>> CreateAuthorAsync(AuthorCreateDto author);
        Task<Response<int>> UpdateAuthorAsync(int id, AuthorUpdateDto author);
        Task<Response<int>> DeleteAuthorAsync(int authorId);

    }
}