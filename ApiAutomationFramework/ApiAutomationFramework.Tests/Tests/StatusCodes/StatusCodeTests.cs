using System.Net;
using ApiAutomationFramework.Tests.Clients;
using ApiAutomationFramework.Tests.Fixtures;
using FluentAssertions;

namespace ApiAutomationFramework.Tests.Tests.StatusCodes;

public class StatusCodeTests : IClassFixture<ApiTestFixture>
{
    private readonly HttpBinClient _client;

    public StatusCodeTests(ApiTestFixture fixture)
    {
        _client = fixture.HttpBinClient;
    }

    [Fact]
    public async Task GetStatus400_ShouldReturnBadRequest()
    {
        var response = await _client.GetStatusAsync(400);

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetStatus401_ShouldReturnUnauthorized()
    {
        var response = await _client.GetStatusAsync(401);

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetStatus404_ShouldReturnNotFound()
    {
        var response = await _client.GetStatusAsync(404);

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetStatus500_ShouldReturnInternalServerError()
    {
        var response = await _client.GetStatusAsync(500);

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.InternalServerError);
    }
}