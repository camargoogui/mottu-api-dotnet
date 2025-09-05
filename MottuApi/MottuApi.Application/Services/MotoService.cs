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
    public class MotoService : IMotoService
    {
        private readonly IMotoRepository _motoRepository;
        private readonly IFilialRepository _filialRepository;
        private readonly IMapper _mapper;

        public MotoService(IMotoRepository motoRepository, IFilialRepository filialRepository, IMapper mapper)
        {
            _motoRepository = motoRepository;
            _filialRepository = filialRepository;
            _mapper = mapper;
        }

        public async Task<MotoDTO> GetByIdAsync(int id)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null)
                throw new DomainException($"Moto com ID {id} não encontrada.");

            return _mapper.Map<MotoDTO>(moto);
        }

        public async Task<MotoDTO> GetByPlacaAsync(string placa)
        {
            var moto = await _motoRepository.GetByPlacaAsync(placa);
            if (moto == null)
                throw new DomainException($"Moto com placa {placa} não encontrada.");

            return _mapper.Map<MotoDTO>(moto);
        }

        public async Task<IEnumerable<MotoDTO>> GetAllAsync()
        {
            var motos = await _motoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MotoDTO>>(motos);
        }

        public async Task<IEnumerable<MotoDTO>> GetByFilialIdAsync(int filialId)
        {
            if (!await _filialRepository.ExistsAsync(filialId))
                throw new DomainException($"Filial com ID {filialId} não encontrada.");

            var motos = await _motoRepository.GetByFilialIdAsync(filialId);
            return _mapper.Map<IEnumerable<MotoDTO>>(motos);
        }

        public async Task<MotoDTO> CreateAsync(CreateMotoDTO createMotoDTO)
        {
            if (await _motoRepository.ExistsByPlacaAsync(createMotoDTO.Placa))
                throw new DomainException($"Já existe uma moto com a placa '{createMotoDTO.Placa}'.");

            var filial = await _filialRepository.GetByIdAsync(createMotoDTO.FilialId);
            if (filial == null)
                throw new DomainException($"Filial com ID {createMotoDTO.FilialId} não encontrada.");

            var moto = _mapper.Map<Moto>(createMotoDTO);
            moto = new Moto(
                createMotoDTO.Placa,
                createMotoDTO.Modelo,
                createMotoDTO.Ano,
                createMotoDTO.Cor,
                filial
            );

            var motoCriada = await _motoRepository.AddAsync(moto);
            return _mapper.Map<MotoDTO>(motoCriada);
        }

        public async Task<MotoDTO> UpdateAsync(int id, UpdateMotoDTO updateMotoDTO)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null)
                throw new DomainException($"Moto com ID {id} não encontrada.");

            moto.Atualizar(
                updateMotoDTO.Modelo,
                updateMotoDTO.Ano,
                updateMotoDTO.Cor
            );

            await _motoRepository.UpdateAsync(moto);
            return _mapper.Map<MotoDTO>(moto);
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _motoRepository.ExistsAsync(id))
                throw new DomainException($"Moto com ID {id} não encontrada.");

            await _motoRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _motoRepository.ExistsAsync(id);
        }

        public async Task<MotoDTO> MarcarComoDisponivelAsync(int id)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null)
                throw new DomainException($"Moto com ID {id} não encontrada.");

            moto.MarcarComoDisponivel();
            await _motoRepository.UpdateAsync(moto);
            return _mapper.Map<MotoDTO>(moto);
        }

        public async Task<MotoDTO> MarcarComoIndisponivelAsync(int id)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null)
                throw new DomainException($"Moto com ID {id} não encontrada.");

            moto.MarcarComoIndisponivel();
            await _motoRepository.UpdateAsync(moto);
            return _mapper.Map<MotoDTO>(moto);
        }
    }
} 