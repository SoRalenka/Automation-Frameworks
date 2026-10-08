using System.Net;
using ApiAutomationFramework.Tests.Clients;
using ApiAutomationFramework.Tests.Fixtures;
using ApiAutomationFramework.Tests.Models;
using FluentAssertions;

namespace ApiAutomationFramework.Tests.Tests.HTTPMethods;

public class PostTests : IClassFixture<ApiTestFixture>
{
    private readonly HttpBinClient _client;

    public PostTests(ApiTestFixture fixture)
    {
        _client = fixture.HttpBinClient;
    }

    [Fact]
    public async Task Post_ShouldReturn200()
    {
        var request = new PostRequest
        {
            Name = "Sora",
            Role = "SDET"
        };

        var response = await _client.PostAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("Sora");
        body.Should().Contain("SDET");
    }
}