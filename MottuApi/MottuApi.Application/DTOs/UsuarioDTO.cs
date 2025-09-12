using System.ComponentModel.DataAnnotations;

namespace MottuApi.Application.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public int? FilialId { get; set; }
        public string? FilialNome { get; set; }
        public List<LinkDTO> Links { get; set; } = new();
    }

    public class CreateUsuarioDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email deve ter formato válido")]
        [StringLength(150, ErrorMessage = "Email deve ter no máximo 150 caracteres")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter exatamente 11 caracteres")]
        public string Cpf { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Telefone é obrigatório")]
        [StringLength(15, ErrorMessage = "Telefone deve ter no máximo 15 caracteres")]
        public string Telefone { get; set; } = string.Empty;
        
        public int? FilialId { get; set; }
    }

    public class UpdateUsuarioDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email deve ter formato válido")]
        [StringLength(150, ErrorMessage = "Email deve ter no máximo 150 caracteres")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Telefone é obrigatório")]
        [StringLength(15, ErrorMessage = "Telefone deve ter no máximo 15 caracteres")]
        public string Telefone { get; set; } = string.Empty;
        
        public int? FilialId { get; set; }
    }

}
