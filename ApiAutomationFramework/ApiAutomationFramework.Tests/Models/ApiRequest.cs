using ApiAutomationFramework.Tests.Models.Authentication;

namespace ApiAutomationFramework.Tests.Models;

public sealed class ApiRequest
{
    public required HttpMethod Method { get; init; }
    public required string Endpoint { get; init; }
    public Dictionary<string, string> QueryParameters { get; init; } = new Dictionary<string, string>();
    public Dictionary<string, string> Headers { get; init; } = new Dictionary<string, string>();
    public object? Body { get; init; }
    public ApiAuthentication? Authentication { get; init; }
}
