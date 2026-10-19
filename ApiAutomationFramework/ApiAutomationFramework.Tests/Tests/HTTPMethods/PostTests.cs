using System.Net;
using System.Text.Json;
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
    public async Task Post_ShouldReturnSentJson()
    {
        var request = new PostRequest
        {
            Name = "Sora",
            Role = "SDET"
        };

        var response = await _client.PostAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseBody =
        await response.Content.ReadAsStringAsync();

        var result =
            JsonSerializer.Deserialize<PostResponse>(responseBody);

        result.Should().NotBeNull();
        result!.Json.Should().NotBeNull();

        result.Json!.Name.Should().Be("Sora");
        result.Json.Role.Should().Be("SDET");
    }
}