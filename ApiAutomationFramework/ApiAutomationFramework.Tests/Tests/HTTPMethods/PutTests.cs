using System.Net;
using ApiAutomationFramework.Tests.Clients;
using ApiAutomationFramework.Tests.Fixtures;
using ApiAutomationFramework.Tests.Models;
using FluentAssertions;

namespace ApiAutomationFramework.Tests.Tests.HTTPMethods;

public class PutTests : IClassFixture<ApiTestFixture>
{
    private readonly HttpBinClient _client;

    public PutTests(ApiTestFixture fixture)
    {
        _client = fixture.HttpBinClient;
    }

    [Fact]
    public async Task Put_ShouldReturn200()
    {
        var request = new PostRequest
        {
            Name = "Sora",
            Role = "SDET"
        };

        var response = await _client.PutAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}