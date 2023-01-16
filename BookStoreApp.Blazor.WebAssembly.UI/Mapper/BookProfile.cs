using AutoMapper;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Mapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookUpdateDto, BookReadOnlyDto>().ReverseMap();
            CreateMap<BookCreateDto, BookReadOnlyDto>().ReverseMap();
            CreateMap<BookCreateDto, BookUpdateDto>().ReverseMap();
            CreateMap<BookDetailsDto, BookUpdateDto>().ReverseMap();
        }
    }
}
