using AutoMapper;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Mapper
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorUpdateDto, AuthorReadOnlyDto>().ReverseMap();
            CreateMap<AuthorCreateDto, AuthorReadOnlyDto>().ReverseMap();
            CreateMap<AuthorCreateDto, AuthorUpdateDto>().ReverseMap();
            CreateMap<AuthorDetailsDto, AuthorUpdateDto>().ReverseMap();
        }
    }
}
