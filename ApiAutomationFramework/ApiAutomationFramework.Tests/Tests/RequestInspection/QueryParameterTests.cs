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

    [Fact]
    public async Task Get_WithCustomHeaders_ShouldEchoHeaders()
    {
        //Arrange
        var headers = new Dictionary<string,string>
        {
            ["X-Custom-Header"] = "CustomValue",
            ["X-Another-Header"] = "AnotherValue"
        };

        var response = await _client.GetHeadersAsync(headers);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<GetResponse>();
        result.Should().NotBeNull();
        result!.Headers["X-Custom-Header"].Should().Be("CustomValue");
    }

    [Fact]
    public async Task Get_WithUserAgent_ShouldEchoUserAgent()
    {
        //Arrange
        var userAgent = "MyCustomUserAgent";
        var response = await _client.GetUserAgentAsync(userAgent);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<UserAgentResponse>();
        result.Should().NotBeNull();
        result!.UserAgent.Should().Be(userAgent);
    }

    [Fact]
    public async Task Get_Ip_ShouldReturnOriginIp()
    {
        //Arrange
        var response = await _client.GetIpAsync();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<IpResponse>();
        result.Should().NotBeNull();
        result!.Origin.Should().NotBeNullOrEmpty();
    }
}