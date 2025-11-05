using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using FluentAssertions;

namespace MottuApi.Tests.Controllers
{
    public class LocacaoMlControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public LocacaoMlControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Add("X-API-KEY", "local-dev-key");
        }

        [Fact]
        public async Task PreverValor_ComParametrosValidos_DeveRetornarOk()
        {
            var response = await _client.GetAsync("/api/v1/locacao/prever-valor?horas=4&anoMoto=2023&valorHora=15");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task PreverValor_ComParametrosInvalidos_DeveRetornarBadRequest()
        {
            var response = await _client.GetAsync("/api/v1/locacao/prever-valor?horas=0&anoMoto=2023&valorHora=15");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task SemApiKey_DeveRetornarUnauthorized()
        {
            var clientSemKey = new WebApplicationFactory<Program>().CreateClient();
            var resp = await clientSemKey.GetAsync("/api/v1/locacao/prever-valor?horas=1&anoMoto=2023&valorHora=10");
            resp.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}


