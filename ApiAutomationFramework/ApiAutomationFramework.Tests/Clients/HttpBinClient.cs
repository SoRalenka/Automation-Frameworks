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

    public async Task<HttpResponseMessage> PatchAsync(PostRequest request)
    {
        var json = JsonSerializer.Serialize(request);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        return await _requestHelper.PatchAsync("/patch", content);
    }

    public async Task<HttpResponseMessage> DeleteAsync()
    {
        return await _requestHelper.DeleteAsync("/delete");
    }

    public async Task<PostResponse> PostAndDeserializeAsync(PostRequest request)
    {
        var response = await PostAsync(request);

        response.EnsureSuccessStatusCode();

        var responseBody =
            await response.Content.ReadAsStringAsync();

        var result =
            JsonSerializer.Deserialize<PostResponse>(
                responseBody);

        return result
            ?? throw new InvalidOperationException(
                "Unable to deserialize POST response.");
    }
    
    public async Task<HttpResponseMessage> GetStatusAsync(int statusCode)
    {
        return await _requestHelper.GetAsync($"/status/{statusCode}");
    }

    public async Task<HttpResponseMessage> GetWithBasicAuthAsync(string username,string password)
    {
        return await _requestHelper.GetWithBasicAuthAsync(
            $"/basic-auth/{username}/{password}",
            username,
            password);
    }

    public async Task<HttpResponseMessage> GetWithBearerTokenAsync(string token)
    {
        return await _requestHelper.GetWithBearerTokenAsync("/bearer",token);
    }

    public async Task<HttpResponseMessage> GetWithDigestAuthAsync(
        string qop,
        string username,
        string password)
    {
        return await _requestHelper.GetWithDigestAuthAsync(
            $"/digest-auth/{qop}/{username}/{password}",
            username,
            password);
    }

    public async Task<HttpResponseMessage> GetDigestAuthWithAlgorithmAsync(
        string qop,
        string username,
        string password,
        string algorithm)
    {
        return await _requestHelper.GetWithDigestAuthAsync(
            $"/digest-auth/{qop}/{username}/{password}/{algorithm}",
            username,
            password);
    }
}