using System.Net;
using ApiAutomationFramework.Tests.Fixtures;
using FluentAssertions;

namespace ApiAutomationFramework.Tests.Tests.HTTPMethods;

public class GetTests : IClassFixture<ApiTestFixture>
{
    private readonly ApiTestFixture _fixture;

    public GetTests(ApiTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Get_ShouldReturn200()
    {
        var response = await _fixture.HttpBinClient.GetAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}