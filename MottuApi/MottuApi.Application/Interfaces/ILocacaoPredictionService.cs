using System.Threading.Tasks;

namespace MottuApi.Application.Interfaces
{
    public interface ILocacaoPredictionService
    {
        Task<decimal> PreverValorTotalAsync(int horas, int anoMoto, decimal valorHora);
    }
}


