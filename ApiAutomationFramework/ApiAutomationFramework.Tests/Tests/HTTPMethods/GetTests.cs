using System.Net;
using ApiAutomationFramework.Tests.Fixtures;
using ApiAutomationFramework.Tests.Clients;
using FluentAssertions;

namespace ApiAutomationFramework.Tests.Tests.HTTPMethods;

public class GetTests : IClassFixture<ApiTestFixture>
{
    private readonly HttpBinClient _client;

    public GetTests(ApiTestFixture fixture)
    {
        _client = fixture.HttpBinClient;
    }

    [Fact]
    public async Task Get_ShouldReturn200()
    {
        var response = await _client.GetAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}