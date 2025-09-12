using System.ComponentModel.DataAnnotations;

namespace MottuApi.Domain.Entities
{
    public class Locacao
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "MotoId é obrigatório")]
        public int MotoId { get; set; }
        public Moto Moto { get; set; } = null!;
        
        [Required(ErrorMessage = "FilialId é obrigatório")]
        public int FilialId { get; set; }
        public Filial Filial { get; set; } = null!;
        
        [Required(ErrorMessage = "Nome do cliente é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome do cliente deve ter no máximo 100 caracteres")]
        public string ClienteNome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "CPF do cliente é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter exatamente 11 caracteres")]
        public string ClienteCpf { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Telefone do cliente é obrigatório")]
        [StringLength(15, ErrorMessage = "Telefone deve ter no máximo 15 caracteres")]
        public string ClienteTelefone { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Data de início é obrigatória")]
        public DateTime DataInicio { get; set; }
        
        public DateTime? DataFim { get; set; }
        
        [Required(ErrorMessage = "Valor por hora é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor por hora deve ser maior que zero")]
        public decimal ValorHora { get; set; }
        
        public decimal? ValorTotal { get; set; }
        
        public StatusLocacao Status { get; set; } = StatusLocacao.Solicitada;
        
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataAtualizacao { get; set; }
        
        // Métodos de domínio
        public void Iniciar()
        {
            if (Status != StatusLocacao.Solicitada)
                throw new InvalidOperationException("Apenas locações solicitadas podem ser iniciadas");
            
            Status = StatusLocacao.Iniciada;
            DataAtualizacao = DateTime.UtcNow;
        }
        
        public void Finalizar()
        {
            if (Status != StatusLocacao.Iniciada)
                throw new InvalidOperationException("Apenas locações iniciadas podem ser finalizadas");
            
            Status = StatusLocacao.Finalizada;
            DataFim = DateTime.UtcNow;
            DataAtualizacao = DateTime.UtcNow;
            CalcularValorTotal();
        }
        
        public void Cancelar()
        {
            if (Status == StatusLocacao.Finalizada)
                throw new InvalidOperationException("Locações finalizadas não podem ser canceladas");
            
            Status = StatusLocacao.Cancelada;
            DataAtualizacao = DateTime.UtcNow;
        }
        
        public void CalcularValorTotal()
        {
            if (DataFim.HasValue)
            {
                var duracao = DataFim.Value - DataInicio;
                var horas = (decimal)duracao.TotalHours;
                ValorTotal = horas * ValorHora;
            }
        }
        
        public bool EstaAtiva()
        {
            return Status == StatusLocacao.Iniciada;
        }
        
        public bool EstaFinalizada()
        {
            return Status == StatusLocacao.Finalizada;
        }
        
        public bool PodeSerIniciada()
        {
            return Status == StatusLocacao.Solicitada;
        }
        
        public bool PodeSerFinalizada()
        {
            return Status == StatusLocacao.Iniciada;
        }
    }
    
    public enum StatusLocacao
    {
        Solicitada = 1,
        Iniciada = 2,
        Finalizada = 3,
        Cancelada = 4
    }
}
