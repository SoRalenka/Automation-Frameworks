using System.Net.Http.Headers;
using System.Text;
using System.Net;
using System.Text.Json;
using ApiAutomationFramework.Tests.Models;
using ApiAutomationFramework.Tests.Models.Authentication;

namespace ApiAutomationFramework.Tests.Helpers;

public class ApiRequestHelper
{
    private readonly HttpClient _httpClient;

    public ApiRequestHelper(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("HttpBin");
    }
    
    public async Task<HttpResponseMessage> SendAsync(ApiRequest apiRequest)
    {
        if(apiRequest.Authentication is DigestAuthentication digestAuth)
        {
            return await SendWithDigestAsync(
                apiRequest,digestAuth);
        }
        var endpoint = BuildEndpoint(apiRequest.Endpoint, apiRequest.QueryParameters);
        var request = new HttpRequestMessage(apiRequest.Method, endpoint);
        AddHeaders(request , apiRequest.Headers);
        AddAuthentication(request , apiRequest.Authentication);
        if(apiRequest.Body != null)
        {
            var json = JsonSerializer.Serialize(apiRequest.Body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }
        return await _httpClient.SendAsync(request);
    }

    private static string BuildEndpoint(string endpoint, Dictionary<string, string>? queryParameters)
    {
        if (queryParameters == null || queryParameters.Count == 0)
        {
            return endpoint;
        }
        var queryString = string.Join(
            "&",
            queryParameters.Select(parameter => $"{Uri.EscapeDataString(parameter.Key)}=" +
                $"{Uri.EscapeDataString(parameter.Value)}"));
        var separator = endpoint.Contains("?") ? "&" : "?";

        return $"{endpoint}{separator}{queryString}";
    }

    private static void AddHeaders(
        HttpRequestMessage request,
        IDictionary<string, string>? headers)
    {
        foreach (var header in headers)
        {
            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }
    }
    
    private static void AddAuthentication(HttpRequestMessage request, ApiAuthentication? authentication)
    {
        switch (authentication)
        {
            case null:
                break;
            case BasicAuthentication basicAuth:
                var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{basicAuth.Username}:{basicAuth.Password}"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic",credentials);
                break;
            case BearerAuthentication bearerAuth:
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerAuth.Token);
                break;    
        }
    }

    private async Task<HttpResponseMessage> SendWithDigestAsync(ApiRequest apiRequest, DigestAuthentication digestAuth)
    {
        using var handler = new HttpClientHandler
        {
            Credentials = new NetworkCredential(digestAuth.Username, digestAuth.Password),
            PreAuthenticate = false
        };
        using var client = new HttpClient(handler)
        {
            BaseAddress = _httpClient.BaseAddress
        };

        var endpoint = BuildEndpoint(apiRequest.Endpoint, apiRequest.QueryParameters);
        using var request = new HttpRequestMessage(apiRequest.Method,endpoint);
        AddHeaders(request, apiRequest.Headers);
        if(apiRequest.Body is not null)
        {
            var json = JsonSerializer.Serialize(apiRequest.Body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        return await client.SendAsync(request);
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