using System.Net;
using System.Text.Json;
using ApiAutomationFramework.Tests.Clients;
using ApiAutomationFramework.Tests.Fixtures;
using ApiAutomationFramework.Tests.Models;
using FluentAssertions;

namespace ApiAutomationFramework.Tests.Tests.Auth;

public class BasicAuthTests : IClassFixture<ApiTestFixture>
{
    private readonly HttpBinClient _client;

    public BasicAuthTests(ApiTestFixture fixture)
    {
        _client = fixture.HttpBinClient;
    }

    [Fact]
    public async Task BasicAuth_WithValidCredentials_ShouldReturn200()
    {
        var response = await _client.GetWithBasicAuthAsync(
            "testuser",
            "testpassword");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        var result =
            JsonSerializer.Deserialize<BasicAuthResponse>(body);

        result.Should().NotBeNull();

        result!.Authenticated.Should().BeTrue();
        result.User.Should().Be("testuser");
    }

    [Fact]
    public async Task BasicAuth_WithInvalidCredentials_ShouldReturn401()
    {
        var response = await _client.GetWithBasicAuthAsync(
            "testuser",
            "wrongpassword");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}