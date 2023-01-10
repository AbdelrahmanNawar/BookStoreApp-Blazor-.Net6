using AutoMapper;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Mapper
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorUpdateDto, AuthorReadOnlyDto>().ReverseMap();
            CreateMap<AuthorCreateDto, AuthorReadOnlyDto>().ReverseMap();
            CreateMap<AuthorCreateDto, AuthorUpdateDto>().ReverseMap();
        }
    }
}
