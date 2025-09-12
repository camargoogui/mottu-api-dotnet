using System.ComponentModel.DataAnnotations;

namespace MottuApi.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        
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
        
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataAtualizacao { get; set; }
        
        // Relacionamento com Filial
        public int? FilialId { get; set; }
        public Filial? Filial { get; set; }
        
        // Métodos de domínio
        public void Ativar()
        {
            Ativo = true;
            DataAtualizacao = DateTime.UtcNow;
        }
        
        public void Desativar()
        {
            Ativo = false;
            DataAtualizacao = DateTime.UtcNow;
        }
        
        public void Atualizar(string nome, string email, string telefone)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}
