using System;
using System.Collections.Generic;
using MottuApi.Domain.ValueObjects;
using MottuApi.Domain.Exceptions;

namespace MottuApi.Domain.Entities
{
    public class Filial
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public Endereco Endereco { get; private set; }
        public string Telefone { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }
        public ICollection<Moto> Motos { get; private set; }

        protected Filial() { } // Para o EF Core

        public Filial(string nome, Endereco endereco, string telefone)
        {
            ValidarNome(nome);
            ValidarTelefone(telefone);

            Nome = nome;
            Endereco = endereco;
            Telefone = telefone;
            Ativo = true;
            DataCriacao = DateTime.UtcNow;
            Motos = new List<Moto>();
        }

        public void Atualizar(string nome, Endereco endereco, string telefone)
        {
            ValidarNome(nome);
            ValidarTelefone(telefone);

            Nome = nome;
            Endereco = endereco;
            Telefone = telefone;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Desativar()
        {
            if (!Ativo)
                throw new DomainException("Filial já está desativada.");

            Ativo = false;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Ativar()
        {
            if (Ativo)
                throw new DomainException("Filial já está ativa.");

            Ativo = true;
            DataAtualizacao = DateTime.UtcNow;
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("Nome da filial não pode ser vazio.");

            if (nome.Length < 3 || nome.Length > 100)
                throw new DomainException("Nome da filial deve ter entre 3 e 100 caracteres.");
        }

        private void ValidarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                throw new DomainException("Telefone não pode ser vazio.");

            // Implementar validação mais robusta de telefone se necessário
            if (telefone.Length < 10 || telefone.Length > 15)
                throw new DomainException("Telefone inválido.");
        }
    }
} 