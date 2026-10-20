using System.Text.Json.Serialization;
namespace ApiAutomationFramework.Tests.Models;

public class BearerAuthResponse
{
    [JsonPropertyName("authenticated")]
    public bool Authenticated { get; set; }

    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
}