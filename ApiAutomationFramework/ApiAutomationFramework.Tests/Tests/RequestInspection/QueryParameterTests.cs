using System.Net;
using System.Net.Http.Json;
using ApiAutomationFramework.Tests.Clients;
using ApiAutomationFramework.Tests.Fixtures;
using ApiAutomationFramework.Tests.Models;
using FluentAssertions;

namespace ApiAutomationFramework.Tests.Tests.RequestInspection;

public class QueryParameterTests : IClassFixture<ApiTestFixture>
{
    private readonly HttpBinClient _client;

    public QueryParameterTests(ApiTestFixture fixture)
    {
        _client = fixture.HttpBinClient;
    }

    [Fact]
    public async Task Get_WithQueryParameters_ShouldEchoParameters()
    {
        //Arrange
        var queryParameters = new Dictionary<string,string>
        {
            ["name"] = "Sourabh",
            ["role"] = "SDET II",
            ["framework"] = "Api automation"
        };

        var response = await _client.GetWithQueryParametersAsync( queryParameters);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<GetResponse>();
        result.Should().NotBeNull();
        result!.Args["name"].Should().Be("Sourabh");
    }
}   