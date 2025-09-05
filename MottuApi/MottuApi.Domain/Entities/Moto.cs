using System;
using MottuApi.Domain.ValueObjects;
using MottuApi.Domain.Exceptions;

namespace MottuApi.Domain.Entities
{
    public class Moto
    {
        public int Id { get; private set; }
        public string Placa { get; private set; }
        public string Modelo { get; private set; }
        public int Ano { get; private set; }
        public string Cor { get; private set; }
        public bool Disponivel { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }
        public int FilialId { get; private set; }
        public Filial Filial { get; private set; }

        protected Moto() { } // Para o EF Core

        public Moto(string placa, string modelo, int ano, string cor, Filial filial)
        {
            ValidarPlaca(placa);
            ValidarModelo(modelo);
            ValidarAno(ano);
            ValidarCor(cor);
            ValidarFilial(filial);

            Placa = placa.ToUpper();
            Modelo = modelo;
            Ano = ano;
            Cor = cor;
            Filial = filial;
            FilialId = filial.Id;
            Disponivel = true;
            DataCriacao = DateTime.UtcNow;
        }

        public void Atualizar(string modelo, int ano, string cor)
        {
            ValidarModelo(modelo);
            ValidarAno(ano);
            ValidarCor(cor);

            Modelo = modelo;
            Ano = ano;
            Cor = cor;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void MarcarComoIndisponivel()
        {
            if (!Disponivel)
                throw new DomainException("Moto já está indisponível.");

            Disponivel = false;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void MarcarComoDisponivel()
        {
            if (Disponivel)
                throw new DomainException("Moto já está disponível.");

            Disponivel = true;
            DataAtualizacao = DateTime.UtcNow;
        }

        private void ValidarPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                throw new DomainException("Placa não pode ser vazia.");

            // Implementar validação mais robusta de placa se necessário
            if (placa.Length != 7)
                throw new DomainException("Placa deve ter 7 caracteres.");
        }

        private void ValidarModelo(string modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo))
                throw new DomainException("Modelo não pode ser vazio.");

            if (modelo.Length < 2 || modelo.Length > 50)
                throw new DomainException("Modelo deve ter entre 2 e 50 caracteres.");
        }

        private void ValidarAno(int ano)
        {
            var anoAtual = DateTime.Now.Year;
            if (ano < 1900 || ano > anoAtual + 1)
                throw new DomainException($"Ano deve estar entre 1900 e {anoAtual + 1}.");
        }

        private void ValidarCor(string cor)
        {
            if (string.IsNullOrWhiteSpace(cor))
                throw new DomainException("Cor não pode ser vazia.");

            if (cor.Length < 3 || cor.Length > 30)
                throw new DomainException("Cor deve ter entre 3 e 30 caracteres.");
        }

        private void ValidarFilial(Filial filial)
        {
            if (filial == null)
                throw new DomainException("Filial é obrigatória.");

            if (!filial.Ativo)
                throw new DomainException("Não é possível cadastrar uma moto em uma filial inativa.");
        }
    }
} 