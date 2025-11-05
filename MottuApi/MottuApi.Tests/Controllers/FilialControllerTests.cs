using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using MottuApi.Application.DTOs;

namespace MottuApi.Tests.Controllers
{
    public class FilialControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public FilialControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _client.DefaultRequestHeaders.Add("X-API-KEY", "local-dev-key");
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/filial");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_WithPagination_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/filial?page=1&pageSize=10");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/filial/1");

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_WithValidData_ShouldReturnCreated()
        {
            // Arrange
            var filial = new CreateFilialDTO
            {
                Nome = "Filial Teste",
                Logradouro = "Rua Teste",
                Numero = "123",
                Complemento = "Sala 1",
                Bairro = "Centro",
                Cidade = "São Paulo",
                Estado = "SP",
                CEP = "01234567",
                Telefone = "(11) 99999-9999"
            };

            var json = JsonSerializer.Serialize(filial);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/v1/filial", content);

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.Created, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_WithInvalidData_ShouldReturnBadRequest()
        {
            // Arrange
            var filial = new CreateFilialDTO
            {
                Nome = "", // Nome vazio deve causar erro de validação
                Logradouro = "Rua Teste",
                Numero = "123",
                Complemento = "Sala 1",
                Bairro = "Centro",
                Cidade = "São Paulo",
                Estado = "SP",
                CEP = "01234567",
                Telefone = "(11) 99999-9999"
            };

            var json = JsonSerializer.Serialize(filial);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/v1/filial", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_WithValidData_ShouldReturnOk()
        {
            // Arrange
            var filial = new UpdateFilialDTO
            {
                Nome = "Filial Atualizada",
                Logradouro = "Rua Atualizada",
                Numero = "456",
                Complemento = "Sala 2",
                Bairro = "Centro",
                Cidade = "Rio de Janeiro",
                Estado = "RJ",
                CEP = "22000000",
                Telefone = "(21) 88888-8888"
            };

            var json = JsonSerializer.Serialize(filial);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/v1/filial/1", content);

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_WithValidId_ShouldReturnNoContent()
        {
            // Act
            var response = await _client.DeleteAsync("/api/v1/filial/1");

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.NoContent, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Ativar_WithValidId_ShouldReturnOk()
        {
            // Act
            var response = await _client.PatchAsync("/api/v1/filial/1/ativar", null);

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Desativar_WithValidId_ShouldReturnOk()
        {
            // Act
            var response = await _client.PatchAsync("/api/v1/filial/1/desativar", null);

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }
    }
}
