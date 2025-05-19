using MottuApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MottuApi.Repositories.Interfaces;

namespace MottuApi.Services
{
    public class MotoService
    {
        private readonly IMotoRepository _repository;
        private readonly IMapper _mapper;
        public MotoService(IMotoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IEnumerable<Moto>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Moto> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<Moto> AddAsync(Moto moto) => _repository.AddAsync(moto);
        public Task<bool> UpdateAsync(Moto moto) => _repository.UpdateAsync(moto);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
