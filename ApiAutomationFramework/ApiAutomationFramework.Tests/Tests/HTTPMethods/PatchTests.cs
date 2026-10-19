using System.Net;
using ApiAutomationFramework.Tests.Clients;
using ApiAutomationFramework.Tests.Fixtures;
using ApiAutomationFramework.Tests.Models;
using FluentAssertions;

namespace ApiAutomationFramework.Tests.Tests.HTTPMethods;

public class PatchTests : IClassFixture<ApiTestFixture>
{
    private readonly HttpBinClient _client;

    public PatchTests(ApiTestFixture fixture)
    {
        _client = fixture.HttpBinClient;
    }

    [Fact]
    public async Task Patch_ShouldReturn200()
    {
        var request = new PostRequest
        {
            Name = "Sora",
            Role = "Senior SDET"
        };

        var response = await _client.PatchAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}