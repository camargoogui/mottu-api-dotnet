using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MottuApi.Application.DTOs;
using MottuApi.Application.Interfaces;
using MottuApi.Domain.Entities;
using MottuApi.Domain.Interfaces;
using MottuApi.Domain.Exceptions;

namespace MottuApi.Application.Services
{
    public class FilialService : IFilialService
    {
        private readonly IFilialRepository _filialRepository;
        private readonly IMapper _mapper;

        public FilialService(IFilialRepository filialRepository, IMapper mapper)
        {
            _filialRepository = filialRepository;
            _mapper = mapper;
        }

        public async Task<FilialDTO> GetByIdAsync(int id)
        {
            var filial = await _filialRepository.GetByIdAsync(id);
            if (filial == null)
                throw new DomainException($"Filial com ID {id} não encontrada.");

            return _mapper.Map<FilialDTO>(filial);
        }

        public async Task<IEnumerable<FilialDTO>> GetAllAsync()
        {
            var filiais = await _filialRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FilialDTO>>(filiais);
        }

        public async Task<PagedResultDTO<FilialDTO>> GetAllPagedAsync(int page, int pageSize)
        {
            var totalCount = await _filialRepository.GetCountAsync();
            var filiais = await _filialRepository.GetPagedAsync(page, pageSize);
            var filiaisDTO = _mapper.Map<IEnumerable<FilialDTO>>(filiais);

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var result = new PagedResultDTO<FilialDTO>
            {
                Data = filiaisDTO.ToList(),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNext = page < totalPages,
                HasPrevious = page > 1
            };

            // Adicionar links HATEOAS
            result.Links.Add(new LinkDTO { Href = $"/api/filial?page={page}&pageSize={pageSize}", Rel = "self", Method = "GET" });
            if (result.HasNext)
                result.Links.Add(new LinkDTO { Href = $"/api/filial?page={page + 1}&pageSize={pageSize}", Rel = "next", Method = "GET" });
            if (result.HasPrevious)
                result.Links.Add(new LinkDTO { Href = $"/api/filial?page={page - 1}&pageSize={pageSize}", Rel = "previous", Method = "GET" });

            return result;
        }

        public async Task<FilialDTO> CreateAsync(CreateFilialDTO createFilialDTO)
        {
            if (await _filialRepository.ExistsByNomeAsync(createFilialDTO.Nome))
                throw new DomainException($"Já existe uma filial com o nome '{createFilialDTO.Nome}'.");

            var filial = _mapper.Map<Filial>(createFilialDTO);
            var filialCriada = await _filialRepository.AddAsync(filial);
            return _mapper.Map<FilialDTO>(filialCriada);
        }

        public async Task<FilialDTO> UpdateAsync(int id, UpdateFilialDTO updateFilialDTO)
        {
            var filial = await _filialRepository.GetByIdAsync(id);
            if (filial == null)
                throw new DomainException($"Filial com ID {id} não encontrada.");

            filial.Atualizar(
                updateFilialDTO.Nome,
                new Domain.ValueObjects.Endereco(
                    updateFilialDTO.Logradouro,
                    updateFilialDTO.Numero,
                    updateFilialDTO.Complemento,
                    updateFilialDTO.Bairro,
                    updateFilialDTO.Cidade,
                    updateFilialDTO.Estado,
                    updateFilialDTO.CEP
                ),
                updateFilialDTO.Telefone
            );

            await _filialRepository.UpdateAsync(filial);
            return _mapper.Map<FilialDTO>(filial);
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _filialRepository.ExistsAsync(id))
                throw new DomainException($"Filial com ID {id} não encontrada.");

            await _filialRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _filialRepository.ExistsAsync(id);
        }

        public async Task<FilialDTO> AtivarAsync(int id)
        {
            var filial = await _filialRepository.GetByIdAsync(id);
            if (filial == null)
                throw new DomainException($"Filial com ID {id} não encontrada.");

            filial.Ativar();
            await _filialRepository.UpdateAsync(filial);
            return _mapper.Map<FilialDTO>(filial);
        }

        public async Task<FilialDTO> DesativarAsync(int id)
        {
            var filial = await _filialRepository.GetByIdAsync(id);
            if (filial == null)
                throw new DomainException($"Filial com ID {id} não encontrada.");

            filial.Desativar();
            await _filialRepository.UpdateAsync(filial);
            return _mapper.Map<FilialDTO>(filial);
        }
    }
} 