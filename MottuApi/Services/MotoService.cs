using MottuApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MotoService
{
    private readonly IMotoRepository _repository;
    public MotoService(IMotoRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Moto>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Moto> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task<Moto> AddAsync(Moto moto) => _repository.AddAsync(moto);
    public Task<bool> UpdateAsync(Moto moto) => _repository.UpdateAsync(moto);
    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}
