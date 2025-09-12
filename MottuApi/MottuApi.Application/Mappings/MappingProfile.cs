using AutoMapper;
using MottuApi.Domain.Entities;
using MottuApi.Application.DTOs;

namespace MottuApi.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Filial mappings
            CreateMap<Filial, FilialDTO>()
                .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.Endereco.Logradouro))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Endereco.Numero))
                .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => src.Endereco.Complemento))
                .ForMember(dest => dest.Bairro, opt => opt.MapFrom(src => src.Endereco.Bairro))
                .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Endereco.Cidade))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Endereco.Estado))
                .ForMember(dest => dest.CEP, opt => opt.MapFrom(src => src.Endereco.CEP));

            CreateMap<CreateFilialDTO, Filial>()
                .ConstructUsing((src, ctx) => new Filial(
                    src.Nome,
                    new Domain.ValueObjects.Endereco(
                        src.Logradouro,
                        src.Numero,
                        src.Complemento,
                        src.Bairro,
                        src.Cidade,
                        src.Estado,
                        src.CEP
                    ),
                    src.Telefone
                ));

            CreateMap<UpdateFilialDTO, Filial>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => new Domain.ValueObjects.Endereco(
                    src.Logradouro,
                    src.Numero,
                    src.Complemento,
                    src.Bairro,
                    src.Cidade,
                    src.Estado,
                    src.CEP
                )));

            // Moto mappings
            CreateMap<Moto, MotoDTO>()
                .ForMember(dest => dest.FilialNome, opt => opt.MapFrom(src => src.Filial.Nome));

            CreateMap<CreateMotoDTO, Moto>()
                .ForMember(dest => dest.Filial, opt => opt.Ignore())
                .ForMember(dest => dest.FilialId, opt => opt.MapFrom(src => src.FilialId));

            CreateMap<UpdateMotoDTO, Moto>();

            // Locacao mappings
            CreateMap<Locacao, LocacaoDTO>()
                .ForMember(dest => dest.MotoPlaca, opt => opt.MapFrom(src => src.Moto.Placa))
                .ForMember(dest => dest.MotoModelo, opt => opt.MapFrom(src => src.Moto.Modelo))
                .ForMember(dest => dest.FilialNome, opt => opt.MapFrom(src => src.Filial.Nome));

            CreateMap<CreateLocacaoDTO, Locacao>();
            CreateMap<UpdateLocacaoDTO, Locacao>();
        }
    }
} 