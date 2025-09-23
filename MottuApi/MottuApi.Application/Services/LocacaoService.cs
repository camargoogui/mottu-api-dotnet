using AutoMapper;
using MottuApi.Application.DTOs;
using MottuApi.Application.Interfaces;
using MottuApi.Domain.Entities;
using MottuApi.Domain.Interfaces;

namespace MottuApi.Application.Services
{
    public class LocacaoService : ILocacaoService
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IMapper _mapper;

        public LocacaoService(ILocacaoRepository locacaoRepository, IMapper mapper)
        {
            _locacaoRepository = locacaoRepository;
            _mapper = mapper;
        }

        public async Task<PagedResultDTO<LocacaoDTO>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            var locacoes = await _locacaoRepository.GetAllAsync(page, pageSize);
            var totalCount = await _locacaoRepository.GetTotalCountAsync();
            
            var locacaoDTOs = _mapper.Map<IEnumerable<LocacaoDTO>>(locacoes);
            
            // Adicionar HATEOAS
            foreach (var locacao in locacaoDTOs)
            {
                locacao.Links = GenerateLinks(locacao.Id);
            }

            return new PagedResultDTO<LocacaoDTO>
            {
                Data = locacaoDTOs.ToList(),
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                HasNext = page < (int)Math.Ceiling((double)totalCount / pageSize),
                HasPrevious = page > 1,
                Links = new List<LinkDTO>
                {
                    new LinkDTO { Href = $"/api/locacao?page={page}&pageSize={pageSize}", Rel = "self", Method = "GET" }
                }
            };
        }

        public async Task<LocacaoDTO?> GetByIdAsync(int id)
        {
            var locacao = await _locacaoRepository.GetByIdAsync(id);
            if (locacao == null) return null;

            var locacaoDTO = _mapper.Map<LocacaoDTO>(locacao);
            locacaoDTO.Links = GenerateLinks(locacaoDTO.Id);
            return locacaoDTO;
        }

        public async Task<IEnumerable<LocacaoDTO>> GetByMotoIdAsync(int motoId)
        {
            var locacoes = await _locacaoRepository.GetByMotoIdAsync(motoId);
            var locacaoDTOs = _mapper.Map<IEnumerable<LocacaoDTO>>(locacoes);
            
            foreach (var locacao in locacaoDTOs)
            {
                locacao.Links = GenerateLinks(locacao.Id);
            }

            return locacaoDTOs;
        }

        public async Task<IEnumerable<LocacaoDTO>> GetByFilialIdAsync(int filialId)
        {
            var locacoes = await _locacaoRepository.GetByFilialIdAsync(filialId);
            var locacaoDTOs = _mapper.Map<IEnumerable<LocacaoDTO>>(locacoes);
            
            foreach (var locacao in locacaoDTOs)
            {
                locacao.Links = GenerateLinks(locacao.Id);
            }

            return locacaoDTOs;
        }

        public async Task<IEnumerable<LocacaoDTO>> GetByClienteCpfAsync(string cpf)
        {
            var locacoes = await _locacaoRepository.GetByClienteCpfAsync(cpf);
            var locacaoDTOs = _mapper.Map<IEnumerable<LocacaoDTO>>(locacoes);
            
            foreach (var locacao in locacaoDTOs)
            {
                locacao.Links = GenerateLinks(locacao.Id);
            }

            return locacaoDTOs;
        }

        public async Task<IEnumerable<LocacaoDTO>> GetByPeriodoAsync(DateTime inicio, DateTime fim)
        {
            var locacoes = await _locacaoRepository.GetByPeriodoAsync(inicio, fim);
            var locacaoDTOs = _mapper.Map<IEnumerable<LocacaoDTO>>(locacoes);
            
            foreach (var locacao in locacaoDTOs)
            {
                locacao.Links = GenerateLinks(locacao.Id);
            }

            return locacaoDTOs;
        }

        public async Task<IEnumerable<LocacaoDTO>> GetAtivasAsync()
        {
            var locacoes = await _locacaoRepository.GetAtivasAsync();
            var locacaoDTOs = _mapper.Map<IEnumerable<LocacaoDTO>>(locacoes);
            
            foreach (var locacao in locacaoDTOs)
            {
                locacao.Links = GenerateLinks(locacao.Id);
            }

            return locacaoDTOs;
        }

        public async Task<IEnumerable<LocacaoDTO>> GetFinalizadasAsync()
        {
            var locacoes = await _locacaoRepository.GetFinalizadasAsync();
            var locacaoDTOs = _mapper.Map<IEnumerable<LocacaoDTO>>(locacoes);
            
            foreach (var locacao in locacaoDTOs)
            {
                locacao.Links = GenerateLinks(locacao.Id);
            }

            return locacaoDTOs;
        }

        public async Task<LocacaoDTO> CreateAsync(CreateLocacaoDTO createLocacaoDTO)
        {
            // Verificar se a moto está disponível
            var motoDisponivel = await _locacaoRepository.MotoEstaDisponivelAsync(
                createLocacaoDTO.MotoId, 
                createLocacaoDTO.DataInicio, 
                createLocacaoDTO.DataFim);

            if (!motoDisponivel)
                throw new InvalidOperationException("A moto não está disponível no período solicitado");

            var locacao = _mapper.Map<Locacao>(createLocacaoDTO);
            var createdLocacao = await _locacaoRepository.CreateAsync(locacao);
            var locacaoDTO = _mapper.Map<LocacaoDTO>(createdLocacao);
            locacaoDTO.Links = GenerateLinks(locacaoDTO.Id);
            return locacaoDTO;
        }

        public async Task<LocacaoDTO> UpdateAsync(int id, UpdateLocacaoDTO updateLocacaoDTO)
        {
            var existingLocacao = await _locacaoRepository.GetByIdAsync(id);
            if (existingLocacao == null)
                throw new ArgumentException("Locação não encontrada");

            // Verificar se a moto está disponível (excluindo a própria locação)
            var motoDisponivel = await _locacaoRepository.MotoEstaDisponivelAsync(
                existingLocacao.MotoId, 
                updateLocacaoDTO.DataInicio, 
                updateLocacaoDTO.DataFim,
                id);

            if (!motoDisponivel)
                throw new InvalidOperationException("A moto não está disponível no período solicitado");

            _mapper.Map(updateLocacaoDTO, existingLocacao);
            var updatedLocacao = await _locacaoRepository.UpdateAsync(existingLocacao);
            var locacaoDTO = _mapper.Map<LocacaoDTO>(updatedLocacao);
            locacaoDTO.Links = GenerateLinks(locacaoDTO.Id);
            return locacaoDTO;
        }

        public async Task DeleteAsync(int id)
        {
            await _locacaoRepository.DeleteAsync(id);
        }

        public async Task<LocacaoDTO> IniciarAsync(int id)
        {
            var locacao = await _locacaoRepository.GetByIdAsync(id);
            if (locacao == null)
                throw new ArgumentException("Locação não encontrada");

            locacao.Iniciar();
            var updatedLocacao = await _locacaoRepository.UpdateAsync(locacao);
            var locacaoDTO = _mapper.Map<LocacaoDTO>(updatedLocacao);
            locacaoDTO.Links = GenerateLinks(locacaoDTO.Id);
            return locacaoDTO;
        }

        public async Task<LocacaoDTO> FinalizarAsync(int id)
        {
            var locacao = await _locacaoRepository.GetByIdAsync(id);
            if (locacao == null)
                throw new ArgumentException("Locação não encontrada");

            locacao.Finalizar();
            var updatedLocacao = await _locacaoRepository.UpdateAsync(locacao);
            var locacaoDTO = _mapper.Map<LocacaoDTO>(updatedLocacao);
            locacaoDTO.Links = GenerateLinks(locacaoDTO.Id);
            return locacaoDTO;
        }

        public async Task<LocacaoDTO> CancelarAsync(int id)
        {
            var locacao = await _locacaoRepository.GetByIdAsync(id);
            if (locacao == null)
                throw new ArgumentException("Locação não encontrada");

            locacao.Cancelar();
            var updatedLocacao = await _locacaoRepository.UpdateAsync(locacao);
            var locacaoDTO = _mapper.Map<LocacaoDTO>(updatedLocacao);
            locacaoDTO.Links = GenerateLinks(locacaoDTO.Id);
            return locacaoDTO;
        }

        public async Task<decimal> CalcularValorTotalAsync(int id)
        {
            var locacao = await _locacaoRepository.GetByIdAsync(id);
            if (locacao == null)
                throw new ArgumentException("Locação não encontrada");

            locacao.CalcularValorTotal();
            await _locacaoRepository.UpdateAsync(locacao);
            return locacao.ValorTotal ?? 0;
        }

        public async Task<bool> MotoEstaDisponivelAsync(int motoId, DateTime dataInicio, DateTime? dataFim, int? excludeLocacaoId = null)
        {
            return await _locacaoRepository.MotoEstaDisponivelAsync(motoId, dataInicio, dataFim, excludeLocacaoId);
        }

        private List<LinkDTO> GenerateLinks(int id)
        {
            return new List<LinkDTO>
            {
                new() { Href = $"/api/locacao/{id}", Rel = "self", Method = "GET" },
                new() { Href = $"/api/locacao/{id}", Rel = "update", Method = "PUT" },
                new() { Href = $"/api/locacao/{id}", Rel = "delete", Method = "DELETE" },
                new() { Href = $"/api/locacao/{id}/iniciar", Rel = "iniciar", Method = "PATCH" },
                new() { Href = $"/api/locacao/{id}/finalizar", Rel = "finalizar", Method = "PATCH" },
                new() { Href = $"/api/locacao/{id}/cancelar", Rel = "cancelar", Method = "PATCH" },
                new() { Href = $"/api/locacao/{id}/calcular-valor", Rel = "calcular-valor", Method = "GET" }
            };
        }
    }
}
