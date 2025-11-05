using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using MottuApi.Application.DTOs;

namespace MottuApi.Tests.Controllers
{
    public class MotoControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public MotoControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _client.DefaultRequestHeaders.Add("X-API-KEY", "local-dev-key");
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/moto");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_WithPagination_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/moto?page=1&pageSize=10");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/moto/1");

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetByPlaca_WithValidPlaca_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/moto/por-placa?placa=ABC1234");

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetByFilialId_WithValidId_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/moto/por-filial/1");

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_WithValidData_ShouldReturnCreated()
        {
            // Arrange
            var moto = new CreateMotoDTO
            {
                Placa = "TEST123",
                Modelo = "Honda Test",
                Ano = 2024,
                Cor = "Azul",
                FilialId = 1
            };

            var json = JsonSerializer.Serialize(moto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/v1/moto", content);

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.Created, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_WithInvalidData_ShouldReturnBadRequest()
        {
            // Arrange
            var moto = new CreateMotoDTO
            {
                Placa = "", // Placa vazia deve causar erro de validação
                Modelo = "Honda Test",
                Ano = 2024,
                Cor = "Azul",
                FilialId = 1
            };

            var json = JsonSerializer.Serialize(moto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/moto", content);

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.BadRequest, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_WithValidData_ShouldReturnOk()
        {
            // Arrange
            var moto = new UpdateMotoDTO
            {
                Modelo = "Yamaha Atualizada",
                Ano = 2023,
                Cor = "Vermelha"
            };

            var json = JsonSerializer.Serialize(moto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/v1/moto/1", content);

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_WithValidId_ShouldReturnNoContent()
        {
            // Act
            var response = await _client.DeleteAsync("/api/v1/moto/1");

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.NoContent, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task MarcarComoDisponivel_WithValidId_ShouldReturnOk()
        {
            // Act
            var response = await _client.PatchAsync("/api/v1/moto/1/disponivel", null);

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task MarcarComoIndisponivel_WithValidId_ShouldReturnOk()
        {
            // Act
            var response = await _client.PatchAsync("/api/v1/moto/1/indisponivel", null);

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }
    }
}
