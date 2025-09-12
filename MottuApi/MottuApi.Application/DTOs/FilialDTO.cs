using System;
using System.ComponentModel.DataAnnotations;

namespace MottuApi.Application.DTOs
{
    public class FilialDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }

    public class LinkDTO
    {
        public string Href { get; set; } = string.Empty;
        public string Rel { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
    }

    public class PagedResultDTO<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }

    public class CreateFilialDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Logradouro é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Logradouro deve ter entre 3 e 100 caracteres")]
        public string Logradouro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Número é obrigatório")]
        [StringLength(10, ErrorMessage = "Número deve ter no máximo 10 caracteres")]
        public string Numero { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Complemento deve ter no máximo 50 caracteres")]
        public string Complemento { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bairro é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Bairro deve ter entre 2 e 50 caracteres")]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cidade é obrigatória")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Cidade deve ter entre 2 e 50 caracteres")]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "Estado é obrigatório")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Estado deve ter 2 caracteres")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "CEP é obrigatório")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP deve ter 8 dígitos")]
        public string CEP { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Telefone deve ter entre 10 e 15 caracteres")]
        public string Telefone { get; set; } = string.Empty;
    }

    public class UpdateFilialDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Logradouro é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Logradouro deve ter entre 3 e 100 caracteres")]
        public string Logradouro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Número é obrigatório")]
        [StringLength(10, ErrorMessage = "Número deve ter no máximo 10 caracteres")]
        public string Numero { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Complemento deve ter no máximo 50 caracteres")]
        public string Complemento { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bairro é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Bairro deve ter entre 2 e 50 caracteres")]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cidade é obrigatória")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Cidade deve ter entre 2 e 50 caracteres")]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "Estado é obrigatório")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Estado deve ter 2 caracteres")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "CEP é obrigatório")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP deve ter 8 dígitos")]
        public string CEP { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Telefone deve ter entre 10 e 15 caracteres")]
        public string Telefone { get; set; } = string.Empty;
    }
} 