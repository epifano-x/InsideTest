using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using InsideTest;
using InsideTest.Domain.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace InsideTest.IntegrationTests
{
    public class PedidoIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PedidoIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostPedido_DeveRetornar201ComPedidoCriado()
        {
            var response = await _client.PostAsync("/api/pedidos", null);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var pedido = await response.Content.ReadFromJsonAsync<JsonElement>();
            Assert.True(pedido.TryGetProperty("id", out _));
        }

        [Fact]
        public async Task GetPedidoById_DeveRetornarPedido()
        {
            var pedidoResponse = await _client.PostAsync("/api/pedidos", null);
            var pedidoJson = await pedidoResponse.Content.ReadFromJsonAsync<JsonElement>();
            var pedidoId = pedidoJson.GetProperty("id").GetGuid();

            var getResponse = await _client.GetAsync($"/api/pedidos/{pedidoId}");

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            var json = await getResponse.Content.ReadFromJsonAsync<JsonElement>();
            Assert.Equal(pedidoId.ToString(), json.GetProperty("id").GetString());
        }
    }
}
