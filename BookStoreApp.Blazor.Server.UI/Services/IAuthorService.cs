using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
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