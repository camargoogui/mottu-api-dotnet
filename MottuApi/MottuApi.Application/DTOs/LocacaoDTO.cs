using System.ComponentModel.DataAnnotations;
using MottuApi.Domain.Entities;

namespace MottuApi.Application.DTOs
{
    public class LocacaoDTO
    {
        public int Id { get; set; }
        public int MotoId { get; set; }
        public string MotoPlaca { get; set; } = string.Empty;
        public string MotoModelo { get; set; } = string.Empty;
        public int FilialId { get; set; }
        public string FilialNome { get; set; } = string.Empty;
        public string ClienteNome { get; set; } = string.Empty;
        public string ClienteCpf { get; set; } = string.Empty;
        public string ClienteTelefone { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public decimal ValorHora { get; set; }
        public decimal? ValorTotal { get; set; }
        public StatusLocacao Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public List<LinkDTO> Links { get; set; } = new();
    }

    public class CreateLocacaoDTO
    {
        [Required(ErrorMessage = "MotoId é obrigatório")]
        public int MotoId { get; set; }
        
        [Required(ErrorMessage = "FilialId é obrigatório")]
        public int FilialId { get; set; }
        
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
    }

    public class UpdateLocacaoDTO
    {
        [Required(ErrorMessage = "Nome do cliente é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome do cliente deve ter no máximo 100 caracteres")]
        public string ClienteNome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Telefone do cliente é obrigatório")]
        [StringLength(15, ErrorMessage = "Telefone deve ter no máximo 15 caracteres")]
        public string ClienteTelefone { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Data de início é obrigatória")]
        public DateTime DataInicio { get; set; }
        
        public DateTime? DataFim { get; set; }
        
        [Required(ErrorMessage = "Valor por hora é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor por hora deve ser maior que zero")]
        public decimal ValorHora { get; set; }
    }

}
