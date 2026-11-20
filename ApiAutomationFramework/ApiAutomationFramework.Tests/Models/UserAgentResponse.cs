using System.Text.Json.Serialization;

namespace ApiAutomationFramework.Tests.Models;

public class UserAgentResponse
{
    [JsonPropertyName("user-agent")]
    public string UserAgent { get; set; } = string.Empty;
}