using AutoMapper;
using Library.Application.DTOs;
using Library.Domain.Entities;

namespace Library.Application.Mappings
{
    public class AuthorMappingConfiguration : Profile
    {
        public AuthorMappingConfiguration()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}