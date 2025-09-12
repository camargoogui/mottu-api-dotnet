using System;
using System.ComponentModel.DataAnnotations;

namespace MottuApi.Application.DTOs
{
    public class MotoDTO
    {
        public int Id { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Cor { get; set; } = string.Empty;
        public bool Disponivel { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public int FilialId { get; set; }
        public string FilialNome { get; set; } = string.Empty;
        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }

    public class CreateMotoDTO
    {
        [Required(ErrorMessage = "Placa é obrigatória")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Placa deve ter 7 caracteres")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Modelo é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Modelo deve ter entre 2 e 50 caracteres")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ano é obrigatório")]
        [Range(1900, 2030, ErrorMessage = "Ano deve estar entre 1900 e 2030")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "Cor é obrigatória")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Cor deve ter entre 3 e 30 caracteres")]
        public string Cor { get; set; } = string.Empty;

        [Required(ErrorMessage = "FilialId é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "FilialId deve ser maior que 0")]
        public int FilialId { get; set; }
    }

    public class UpdateMotoDTO
    {
        [Required(ErrorMessage = "Modelo é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Modelo deve ter entre 2 e 50 caracteres")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ano é obrigatório")]
        [Range(1900, 2030, ErrorMessage = "Ano deve estar entre 1900 e 2030")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "Cor é obrigatória")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Cor deve ter entre 3 e 30 caracteres")]
        public string Cor { get; set; } = string.Empty;
    }
} 