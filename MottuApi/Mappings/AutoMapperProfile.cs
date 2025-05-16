using AutoMapper;
using MottuApi.Models;
using MottuApi.DTOs;

namespace MottuApi.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Moto, MotoDto>()
                .ForMember(dest => dest.FilialNome, opt => opt.MapFrom(src => src.Filial.Nome));
            CreateMap<MotoDto, Moto>();
            CreateMap<Filial, FilialDto>();
            CreateMap<FilialDto, Filial>();
        }
    }
}
