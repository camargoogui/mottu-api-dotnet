using FluentAssertions;
using MottuApi.Domain.Entities;
using MottuApi.Domain.Enums;
using MottuApi.Domain.Exceptions;
using MottuApi.Domain.ValueObjects;

namespace MottuApi.Tests.Domain
{
    public class DomainRulesTests
    {
        [Fact]
        public void Filial_AtivarDesativar_DeveAlternarEstado()
        {
            var filial = new Filial("Filial X", new Endereco("Rua A","1","","Bairro","Cidade","SP","01234567"), "11999999999");
            filial.Ativo.Should().BeTrue();
            filial.Desativar();
            filial.Ativo.Should().BeFalse();
            filial.Invoking(f => f.Desativar()).Should().Throw<DomainException>();
            filial.Ativar();
            filial.Ativo.Should().BeTrue();
        }

        [Fact]
        public void Moto_Status_DisponivelIndisponivel_RegrasDevemSerRespeitadas()
        {
            var filial = new Filial("Filial Y", new Endereco("Rua A","1","","Bairro","Cidade","SP","01234567"), "11999999999");
            var moto = new Moto("ABC1234", "Modelo", 2024, "Preta", filial);
            moto.Status.Should().Be(MotoStatus.Disponivel);
            moto.MarcarComoIndisponivel();
            moto.Status.Should().Be(MotoStatus.Ocupada);
            moto.Invoking(m => m.MarcarComoIndisponivel()).Should().Throw<DomainException>();
            moto.MarcarComoDisponivel();
            moto.Status.Should().Be(MotoStatus.Disponivel);
        }

        [Fact]
        public void Locacao_Finalizar_DeveCalcularValor()
        {
            var loc = new Locacao
            {
                MotoId = 1,
                FilialId = 1,
                ClienteNome = "Cliente",
                ClienteCpf = "12345678901",
                ClienteTelefone = "11999999999",
                DataInicio = DateTime.UtcNow.AddHours(-3),
                ValorHora = 10
            };

            loc.Iniciar();
            loc.Finalizar();
            loc.Status.Should().Be(StatusLocacao.Finalizada);
            loc.ValorTotal.Should().NotBeNull();
            loc.ValorTotal.Should().BeGreaterThan(0);
        }
    }
}


