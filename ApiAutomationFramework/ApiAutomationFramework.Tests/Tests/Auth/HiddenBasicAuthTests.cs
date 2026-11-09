using System.Net;
using System.Text.Json;
using ApiAutomationFramework.Tests.Clients;
using ApiAutomationFramework.Tests.Fixtures;
using ApiAutomationFramework.Tests.Models;
using FluentAssertions;

namespace ApiAutomationFramework.Tests.Tests.Auth;

public class HiddenBasicAuthTests: IClassFixture<ApiTestFixture>
{
    private readonly HttpBinClient _client;

    public HiddenBasicAuthTests(ApiTestFixture fixture)
    {
        _client = fixture.HttpBinClient;
    }

    [Fact]
    public async Task HiddenBasicAuth_WithValidCredentials_ShouldAuthenticateUser()
    {
        const string username = "testuser";
        const string password = "testpassword";

        var response = await _client.GetWithHiddenBasicAuthAsync(
            username,
            password);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<DigestAuthResponse>(body);

        result.Should().NotBeNull();

        result!.Authenticated.Should().BeTrue();
        result.User.Should().Be(username);
    }
}