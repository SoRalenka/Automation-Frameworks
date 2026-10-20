using System.Net;
using System.Text.Json;
using ApiAutomationFramework.Tests.Clients;
using ApiAutomationFramework.Tests.Fixtures;
using ApiAutomationFramework.Tests.Models;
using FluentAssertions;

namespace ApiAutomationFramework.Tests.Tests.Auth;

public class DigestAuthTests : IClassFixture<ApiTestFixture>
{
    private readonly HttpBinClient _client;

    public DigestAuthTests(ApiTestFixture fixture)
    {
        _client = fixture.HttpBinClient;
    }

    [Fact]
    public async Task DigestAuth_WithValidCredentials_ShouldAuthenticateUser()
    {
        const string username = "testuser";
        const string password = "testpassword";

        var response = await _client.GetWithDigestAuthAsync(
            "auth",
            username,
            password);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<DigestAuthResponse>(body);

        result.Should().NotBeNull();

        result!.Authenticated.Should().BeTrue();
        result.User.Should().Be(username);
    }

    [Fact]
    public async Task DigestAuth_MD5_WithValidCredentials_ShouldAuthenticate()
    {
        const string username = "testuser";
        const string password = "testpassword";

        var response =
            await _client.GetDigestAuthWithAlgorithmAsync(
                "auth",
                username,
                password,
                "MD5");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<DigestAuthResponse>(body);

        result.Should().NotBeNull();

        result!.Authenticated.Should().BeTrue();
        result.User.Should().Be(username);
    }
}