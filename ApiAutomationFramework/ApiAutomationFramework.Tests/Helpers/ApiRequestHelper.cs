using System.Net.Http.Headers;
using System.Text;
using System.Net;

public class ApiRequestHelper
{
    private readonly HttpClient _httpClient;

    public ApiRequestHelper(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("HttpBin");
    }

    public async Task<HttpResponseMessage> GetAsync(string endpoint)
    {
        return await _httpClient.GetAsync(endpoint);
    }

    public async Task<HttpResponseMessage> PostAsync(
        string endpoint,
        HttpContent? content = null)
    {
        return await _httpClient.PostAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> PutAsync(
        string endpoint,
        HttpContent? content = null)
    {
        return await _httpClient.PutAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> PatchAsync(
        string endpoint,
        HttpContent? content = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, endpoint)
        {
            Content = content
        };

        return await _httpClient.SendAsync(request);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
    {
        return await _httpClient.DeleteAsync(endpoint);
    }

    public async Task<HttpResponseMessage> GetWithBasicAuthAsync(
        string endpoint,
        string username,
        string password)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            endpoint);

        var credentials =
            Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{username}:{password}"));

        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

        return await _httpClient.SendAsync(request);
    }

    public async Task<HttpResponseMessage> GetWithBearerTokenAsync(
        string endpoint,
        string token)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            endpoint);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await _httpClient.SendAsync(request);
    }

    public async Task<HttpResponseMessage> GetWithDigestAuthAsync(
        string endpoint,
        string username,
        string password)
    {
        using var handler = new HttpClientHandler
        {
            Credentials = new NetworkCredential(username, password),
            PreAuthenticate = false
        };

        using var client = new HttpClient(handler)
        {
            BaseAddress = _httpClient.BaseAddress
        };

        return await client.GetAsync(endpoint);
    }
}