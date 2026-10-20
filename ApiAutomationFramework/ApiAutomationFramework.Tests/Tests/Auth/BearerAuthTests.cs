using System.Net;
using System.Text.Json;
using ApiAutomationFramework.Tests.Clients;
using ApiAutomationFramework.Tests.Fixtures;
using ApiAutomationFramework.Tests.Models;
using FluentAssertions;

namespace ApiAutomationFramework.Tests.Tests.Auth;

public class BearerAuthTests : IClassFixture<ApiTestFixture>
{
    private readonly HttpBinClient _client;

    public BearerAuthTests(ApiTestFixture fixture)
    {
        _client = fixture.HttpBinClient;
    }

    [Fact]
    public async Task BearerAuth_WithValidToken_ShouldAuthenticate()
    {
        const string token = "my-test-token";

        var response =
            await _client.GetWithBearerTokenAsync(token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body =
            await response.Content.ReadAsStringAsync();

        var result =
            JsonSerializer.Deserialize<BearerAuthResponse>(body);

        result.Should().NotBeNull();

        result!.Authenticated.Should().BeTrue();
        result.Token.Should().Be(token);
    }

    [Fact]
    public async Task BearerAuth_WithoutToken_ShouldReturn401()
    {
        var response = await _client.GetWithBearerTokenAsync(string.Empty);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}