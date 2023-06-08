using AutoMapper;
using Library.Application.DTOs;
using Library.Domain.Entities;

namespace Library.Application.Mappings
{
    public class BookMappingConfiguration : Profile
    {
        public BookMappingConfiguration()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}