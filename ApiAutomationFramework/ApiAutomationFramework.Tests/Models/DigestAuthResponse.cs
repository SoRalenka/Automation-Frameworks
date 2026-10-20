using System.Text.Json.Serialization;
namespace ApiAutomationFramework.Tests.Models;

public class DigestAuthResponse
{
    [JsonPropertyName("authenticated")]
    public bool Authenticated { get; set; }

    [JsonPropertyName("user")]
    public string User { get; set; } = string.Empty;
}