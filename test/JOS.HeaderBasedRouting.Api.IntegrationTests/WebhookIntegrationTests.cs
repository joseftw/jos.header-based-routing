using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using Xunit;

namespace JOS.HeaderBasedRouting.Api.IntegrationTests
{
    public class WebhookIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;

        public WebhookIntegrationTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory ?? throw new ArgumentNullException(nameof(webApplicationFactory));
        }

        [Theory]
        [InlineData(WebhookOperation.UserAdded)]
        [InlineData(WebhookOperation.UserUpdated)]
        [InlineData(WebhookOperation.UserRemoved)]
        public async Task ShouldUseCorrectActionForWebhookBasedOnOperationHeader(string operation)
        {
            var client = _webApplicationFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "webhook")
            {
                Headers = {{WebhookHeader.Operation, operation}}
            };

            var response = await client.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            responseBody.ShouldBe($"Hello from {operation} action");
        }
    }
}
