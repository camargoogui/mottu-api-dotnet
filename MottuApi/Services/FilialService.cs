using MottuApi.Models;
using MottuApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MottuApi.Services
{
    public class FilialService
    {
        private readonly IFilialRepository _repository;
        public FilialService(IFilialRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Filial>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Filial> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<Filial> AddAsync(Filial filial) => _repository.AddAsync(filial);
        public Task<bool> UpdateAsync(Filial filial) => _repository.UpdateAsync(filial);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
