using System.Net.Http;
using System.Text;
using System.Text.Json;
using ApiAutomationFramework.Tests.Helpers;
using ApiAutomationFramework.Tests.Models;

namespace ApiAutomationFramework.Tests.Clients;

public class HttpBinClient
{
    private readonly ApiRequestHelper _requestHelper;

    public HttpBinClient(ApiRequestHelper requestHelper)
    {
        _requestHelper = requestHelper;
    }

    public async Task<HttpResponseMessage> GetAsync()
    {
        return await _requestHelper.GetAsync("/get");
    }

    public async Task<HttpResponseMessage> PostAsync(PostRequest request)
    {
        var json = JsonSerializer.Serialize(request);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        return await _requestHelper.PostAsync("/post", content);
    }

    public async Task<HttpResponseMessage> PutAsync(PostRequest request)
    {
        var json = JsonSerializer.Serialize(request);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        return await _requestHelper.PutAsync("/put", content);
    }
}