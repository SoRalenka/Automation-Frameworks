using System.Net;
using ApiAutomationFramework.Tests.Clients;
using ApiAutomationFramework.Tests.Fixtures;
using FluentAssertions;

namespace ApiAutomationFramework.Tests.Tests.HTTPMethods;

public class DeleteTests : IClassFixture<ApiTestFixture>
{
    private readonly HttpBinClient _client;

    public DeleteTests(ApiTestFixture fixture)
    {
        _client = fixture.HttpBinClient;
    }

    [Fact]
    public async Task Delete_ShouldReturn200()
    {
        var response = await _client.DeleteAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}