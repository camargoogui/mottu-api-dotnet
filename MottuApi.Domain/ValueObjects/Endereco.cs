using System;
using MottuApi.Domain.Exceptions;

namespace MottuApi.Domain.ValueObjects
{
    public class Endereco
    {
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string CEP { get; private set; }

        protected Endereco() { } // Para o EF Core

        public Endereco(string logradouro, string numero, string complemento, string bairro, 
                       string cidade, string estado, string cep)
        {
            ValidarLogradouro(logradouro);
            ValidarNumero(numero);
            ValidarBairro(bairro);
            ValidarCidade(cidade);
            ValidarEstado(estado);
            ValidarCEP(cep);

            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado.ToUpper();
            CEP = cep.Replace("-", "");
        }

        private void ValidarLogradouro(string logradouro)
        {
            if (string.IsNullOrWhiteSpace(logradouro))
                throw new DomainException("Logradouro não pode ser vazio.");

            if (logradouro.Length < 3 || logradouro.Length > 100)
                throw new DomainException("Logradouro deve ter entre 3 e 100 caracteres.");
        }

        private void ValidarNumero(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                throw new DomainException("Número não pode ser vazio.");

            if (numero.Length > 10)
                throw new DomainException("Número deve ter no máximo 10 caracteres.");
        }

        private void ValidarBairro(string bairro)
        {
            if (string.IsNullOrWhiteSpace(bairro))
                throw new DomainException("Bairro não pode ser vazio.");

            if (bairro.Length < 2 || bairro.Length > 50)
                throw new DomainException("Bairro deve ter entre 2 e 50 caracteres.");
        }

        private void ValidarCidade(string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
                throw new DomainException("Cidade não pode ser vazia.");

            if (cidade.Length < 2 || cidade.Length > 50)
                throw new DomainException("Cidade deve ter entre 2 e 50 caracteres.");
        }

        private void ValidarEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new DomainException("Estado não pode ser vazio.");

            if (estado.Length != 2)
                throw new DomainException("Estado deve ter 2 caracteres.");
        }

        private void ValidarCEP(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                throw new DomainException("CEP não pode ser vazio.");

            cep = cep.Replace("-", "");
            if (cep.Length != 8)
                throw new DomainException("CEP deve ter 8 dígitos.");

            if (!int.TryParse(cep, out _))
                throw new DomainException("CEP deve conter apenas números.");
        }

        public override string ToString()
        {
            return $"{Logradouro}, {Numero}" + 
                   (string.IsNullOrWhiteSpace(Complemento) ? "" : $" - {Complemento}") +
                   $", {Bairro}, {Cidade}/{Estado}, CEP: {CEP.Insert(5, "-")}";
        }
    }
} 