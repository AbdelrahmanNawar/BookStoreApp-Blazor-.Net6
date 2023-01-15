using AutoMapper;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Mapper
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
