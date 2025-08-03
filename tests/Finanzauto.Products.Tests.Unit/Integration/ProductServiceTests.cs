using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;

namespace Finanzauto.ProductsApi.Test.Application
{
    public class ProductServiceTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductServiceTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task GetLoginOk()
        {
            var loginRequest = new
            {
                Username = "admin",
                Password = "123"
            };

            var response = await _client.PostAsJsonAsync("api/Login", loginRequest);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            body.Should().NotBeNullOrWhiteSpace();
        }
    }
}
